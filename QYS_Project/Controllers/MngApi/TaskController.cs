using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;
using QYS.Service.Service.TaskSvr;
using QYS.Service.Service.TaskSvr.Dto;
using QYS.Service.Tool;
using QYS_Project.Requests;
using QYS_Project.Requests.MngApi.Task;
using QYS_Project.Responses;
using QYS_Project.Responses.MngApi.Task;
using QYS_Project.Responses.Model;

namespace QYS_Project.Controllers.MngApi
{
    [Route("api/mngapi/[controller]")]
    [ApiController]
    public class TaskController
    {
        private readonly ITaskService _taskService;

        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService
            , IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetTasks")]
        public async Task<Response<PageList<TaskListResponse>>> GetMenus([FromBody] TaskQueryRequest request)
        {
            var taskList = await _taskService.GetAllJobs();

            var list = _mapper.Map<List<TaskListResponse>>(taskList);

            var listQuery = list
                .WhereIf(!string.IsNullOrEmpty(request.JobGroup), p => p.JobGroup == request.JobGroup)
                .WhereIf(!string.IsNullOrEmpty(request.JobName), p => p.JobName == request.JobName);

            var taskListResponses = listQuery as TaskListResponse[] ?? listQuery.ToArray();

            var totalCount = taskListResponses.Count();

            var table = taskListResponses.OrderBy(p => p.JobGroup).ThenBy(p => p.JobName)
                .Skip((request.PageInfo.PageIndex - 1) * request.PageInfo.PageSize)
                .Take(request.PageInfo.PageSize).ToList();

            var data = PageList<TaskListResponse>.Create(table, totalCount);

            return ResultBuilder.SuccessResult(data);
        }

        /// <summary>
        /// 手动处理任务  （先做一个手动执行）
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("ManualTask")]
        public async Task<ResponseSimple> ManualTask(ManualTaskRequest request)
        {
            var result = await _taskService.ManualTask(request.JobGroup, request.JobName, request.JobAction);
            return !string.IsNullOrEmpty(result) ? 
                ResultBuilder.SimpleFail(result) : 
                ResultBuilder.SimpleSuccess("请求受理成功");
        }

    }
}
