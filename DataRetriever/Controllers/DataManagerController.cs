using DataRetriever.Services.Interfaces;
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
    public async Task<ActionResult> CreateData([FromBody] CreateDataDto dataItem)
    {
      try
      {
        var value = await _dataRetrieverService.CreateData(dataItem);
        return CreatedAtAction(nameof(GetData), new { id = value.Id }, value);
      }
      catch (Exception ex)
      {
        return Conflict(ex.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateData(Guid id, [FromBody] DataItem dataItem)
    {
      return Ok();
    }
  }
}