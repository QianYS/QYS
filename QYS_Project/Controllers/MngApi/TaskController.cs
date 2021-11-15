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
        [HttpGet, Route("GetTasks")]
        public async Task<Response<List<TaskListResponse>>> GetMenus()
        {
            var taskList = await _taskService.GetAllJobs();

            var list = _mapper.Map<List<TaskListResponse>>(taskList);

            return ResultBuilder.SuccessResult(list);
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
