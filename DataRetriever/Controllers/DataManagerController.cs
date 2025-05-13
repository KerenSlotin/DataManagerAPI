using DataRetriever.Dtos;
using DataRetriever.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataRetriever.Controllers
{
  [Route("data")]
  public class DataManagerController : ControllerBase
  {
    private readonly IDataRetrieverService _dataRetrieverService;

    public DataManagerController(IDataRetrieverService dataRetrieverService)
    {
      _dataRetrieverService = dataRetrieverService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<DataItem>> GetData(string id)
    {
      var data = await _dataRetrieverService.GetDataAsync(id);
      if (data == null)
      {
        return NotFound();
      }

      return Ok(data);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateData([FromBody] CreateDataDto dataItem)
    {
      try
      {
        var value = await _dataRetrieverService.CreateDataAsync(dataItem);
        return CreatedAtAction(nameof(GetData), new { id = value.Id }, value);
      }
      catch (Exception ex)
      {
        return Conflict(ex.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateData(string id, [FromBody] UpdateDataDto dataItem)
    {
      try
      {
        await _dataRetrieverService.UpdateDataAsync(id, dataItem);
        return Ok();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}