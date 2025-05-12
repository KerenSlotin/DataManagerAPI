using DataRetriever.Models;
using Microsoft.AspNetCore.Mvc;

[Route("data")]
public class DataManagerController : ControllerBase
{
    private readonly IDataRetrieverService _dataRetrieverService;

    public DataManagerController(IDataRetrieverService dataRetrieverService)
    {
        _dataRetrieverService = dataRetrieverService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetData(Guid id)
    {
        var data = await _dataRetrieverService.GetDataAsync(id);
        if (data == null)
        {
            return NotFound();
        }

        return Ok(data);
    }

    [HttpPost]
    public async Task<ActionResult> CreateData([FromBody] DataItem dataItem)
    {
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateData(Guid id, [FromBody] DataItem dataItem)
    {
        return Ok();
    }
}