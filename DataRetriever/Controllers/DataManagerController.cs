using DataRetriever.Dtos;
using DataRetriever.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataRetriever.Controllers
{
  /// <summary>
  /// Controller for managing data items.
  /// Provides endpoints for retrieving, creating, and updating data.
  /// </summary>
  [Route("data")]
  public class DataManagerController : ControllerBase
  {
    private readonly IDataRetrieverService _dataRetrieverService;
    private IValidator<CreateDataDto> _createDataValidator;
    private IValidator<UpdateDataDto> _updateDataValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataManagerController"/> class.
    /// </summary>
    public DataManagerController(IDataRetrieverService dataRetrieverService, IValidator<CreateDataDto> validator, IValidator<UpdateDataDto> updateDataValidator)
    {
      _dataRetrieverService = dataRetrieverService;
      _createDataValidator = validator;
      _updateDataValidator = updateDataValidator;
    }

    /// <summary>
    /// Retrieves data by ID.
    /// </summary>
    /// <param name="id"> The unique identifier of the data item. </param>
    /// <returns> Returns data item if found. Otherwise, a 404 Not Found response</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ActionResult<DataItemResponseDto>> GetData(string id)
    {
      var data = await _dataRetrieverService.GetDataAsync(id);
      if (data == null)
      {
        return NotFound();
      }

      return Ok(data);
    }

    /// <summary>
    /// Creates a new data item.
    /// </summary>
    /// <param name="dataItem"> The data item to be created. </param>
    /// <returns> Returns the created data item with a 201 Created response.</returns>
    /// <remarks> The returned ID should be used to retrieve the data. </remarks>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateData([FromBody] CreateDataDto dataItem)
    {
      var result = await _createDataValidator.ValidateAsync(dataItem);
      if (!result.IsValid)
      {
        return BadRequest(result.Errors);
      }
      
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
    
    /// <summary>
    /// Updates an existing data item by ID.
    /// </summary>
    /// <param name="id"> The ID of the data item to update.</param>
    /// <param name="dataItem"> The updated data content.</param>
    /// <returns> Returns 200 OK if the update is successful; otherwise, 400 Bad Request.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateData(string id, [FromBody] UpdateDataDto dataItem)
    {
      var result = await _updateDataValidator.ValidateAsync(dataItem);
      if (!result.IsValid)
      {
        return BadRequest(result.Errors);
      }

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