using Microsoft.AspNetCore.Mvc;

[Route("data")]
public class DataRetrieverController : ControllerBase
{
    private readonly IDataRetrieverService _dataRetrieverService;

    public DataRetrieverController(IDataRetrieverService dataRetrieverService)
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
}