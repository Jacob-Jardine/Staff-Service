﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Staff_Service.DomainModel;
using Staff_Service.DTOs;
using Microsoft.Extensions.Caching.Memory;
using Staff_Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Staff_Service.Controllers
{
    [Route("api/staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<StaffController> _logger;

        public StaffController(IStaffRepository staffRepository, IMapper mapper, ILogger<StaffController> logger) 
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("GetAllStaff")]
        [Authorize("ReadAllStaff")]
        public async Task<ActionResult<IEnumerable<StaffReadDTO>>> GetAllStaff()
        {
            try 
            {
                //if(_memoryCache.TryGetValue("GetAllStaff", out IEnumerable<StaffDomainModel> staffDomainModel))
                //{
                //    return Ok(_mapper.Map<IEnumerable<StaffReadDTO>>(staffDomainModel));
                //}

                var getAllStaff = await _staffRepository.GetAllStaffAsync();
                return Ok(_mapper.Map<IEnumerable<StaffReadDTO>>(getAllStaff));
            }
            catch (Exception _exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{ID}")]
        [Authorize("ReadStaff")]
        public async Task<IActionResult> GetStaffByID(int ID)
        {
            try 
            {
                if(ID < 1)
                {
                    return NotFound("ID can't be less than 1");
                }

                //if (_memoryCache.TryGetValue("GetAllStaff", out IEnumerable<StaffDomainModel> staffDomainModel)) 
                //{
                //    var staffMember = staffDomainModel.FirstOrDefault(x => x.StaffID == ID);
                //    if(staffMember != null)
                //    {
                //        return Ok(_mapper.Map<StaffReadDTO>(staffMember));
                //    }
                //}
                
                var getStaffByID = await _staffRepository.GetStaffByIDAsnyc(ID);
                if (getStaffByID != null) 
                {
                    return Ok(_mapper.Map<StaffReadDTO>(getStaffByID));
                }
                else 
                {
                    return NotFound($"Can't find staff memeber with ID of {ID}");
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("CreateStaff")]
        [Authorize("CreateStaff")]
        public async Task<ActionResult> CreateStaffMember([FromBody] StaffCreateDTO staffCreateDTO) 
        {
            try 
            {
                //if (_memoryCache.TryGetValue("GetAllStaff", out List<StaffDomainModel> staffDomainModel))
                //{
                //    staffCreateDTO.StaffID = staffDomainModel.Max(x => x.StaffID) + 1;
                //    var staffMember = _mapper.Map<StaffDomainModel>(staffCreateDTO);
                //    staffDomainModel.Add(staffMember);
                //    if (staffMember != null)
                //    {
                //        return Ok(_mapper.Map<StaffReadDTO>(staffMember));
                //    }
                //}

                var staffModel = _mapper.Map<StaffDomainModel>(staffCreateDTO);
                await _staffRepository.CreateStaff(staffModel);
                return CreatedAtAction(nameof(GetStaffByID), new { ID = staffModel.StaffID }, staffModel);
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpPut("update/{ID}")]
        [Authorize("UpdateStaff")]
        public async Task<ActionResult> UpdateStaffMemeber([FromBody] StaffUpdateDTO staffUpdateDTO, int ID) 
        {
            if(staffUpdateDTO == null)
            {
                return BadRequest();
            }
            try 
            {
                var staffModel = await _staffRepository.GetStaffByIDAsnyc(ID);
                if (staffModel == null) 
                {
                    return NotFound();
                }
                else
                {
                    var updateStaff = _mapper.Map<StaffUpdateDTO>(staffUpdateDTO);
                    updateStaff.StaffID = staffModel.StaffID;

                    _mapper.Map(updateStaff, staffModel);
                    if (await _staffRepository.UpdateStaff(staffModel) == true)
                    {
                        return Ok($"Staff Member With Id {staffModel.StaffID} has been updated");
                    }
                    else
                    {
                        return BadRequest("Error In Model!");
                    }
                }
            }
            catch 
            {
                return BadRequest();
            }
        }
        
        [HttpDelete("delete/{id}")]
        [Authorize("DeleteStaff")]
        public async Task<ActionResult> DeleteStaffByID(int ID) 
        {
            try
            {
                if (ID < 1)
                {
                    return NotFound("ID can't be less than 1");
                }

                var staffModel = await _staffRepository.GetStaffByIDAsnyc(ID);
                if (staffModel == null)
                {
                    return NotFound($"Can't find staff memeber with ID of {ID}");
                }
                else
                {
                    await _staffRepository.DeleteStaff(staffModel.StaffID);
                    return Ok($"Delete staff memeber with ID {ID}");
                }
            }
            catch
            {
                return BadRequest();
            }        
        }
    }
}
