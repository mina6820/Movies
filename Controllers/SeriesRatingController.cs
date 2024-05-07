using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movies.DTOs.RatingDTOs;
using Movies.Models;
using Movies.Repositories.MovieRatingRepo;
using Movies.Repositories.MovieRepo;
using Movies.Repositories.SeriesRatingRepo;
using Movies.Repositories.SeriesRepo;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesRatingController : ControllerBase
    {
        private readonly ISeriesRatingRepository SeriesRatingrepository;
        private readonly ISeriesRepository seriesRepository;
        public SeriesRatingController(ISeriesRatingRepository _SeriesRatingrepository, ISeriesRepository _seriesRepository)
        {
            SeriesRatingrepository = _SeriesRatingrepository;
            seriesRepository = _seriesRepository;

        }

        [HttpGet("{id}")]
        public ActionResult<GeneralResponse> GetSeriesRating(int id)
        {
            Series SeriesDb = seriesRepository.GetSeries(id);
            if (SeriesDb == null)
                return new GeneralResponse() { IsSuccess = false, Data = $"No Series with ID : {id}" };


            double SeriesRating = SeriesRatingrepository.GetSeriesRating(id);
            return new GeneralResponse() { IsSuccess = true, Data = SeriesRating };
        }

        [HttpPost]
        public ActionResult<GeneralResponse> AddRating(AddSeriesRatingDTO addRatingDTO)
        {
            if (ModelState.IsValid)
            {
                SeriesRating seriesRating = new SeriesRating()
                {
                    Rate = addRatingDTO.Rate,
                    UserID = addRatingDTO.UserID,
                    SeriesID = addRatingDTO.SeriesID,
                    IsDeleted = false,
                };
                SeriesRatingrepository.Insert(seriesRating);
                SeriesRatingrepository.Save();

                return new GeneralResponse() { IsSuccess = true, Data = addRatingDTO };
            }
            else
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Data = " Add Operation Failed "
                };
            }
        }


        [HttpPut]
        [Route("{id}")]
        public ActionResult<GeneralResponse> EditRating(int id, AddSeriesRatingDTO addRatingDTO)
        {
            SeriesRating seriesRating = SeriesRatingrepository.GetById(id);
            if (seriesRating == null)
            {
                return new GeneralResponse() { IsSuccess = false, Data = "No Rating Found" };
            }
            seriesRating.Rate = addRatingDTO.Rate;
            seriesRating.UserID = addRatingDTO.UserID;
            seriesRating.SeriesID= addRatingDTO.SeriesID;

            SeriesRatingrepository.Update(seriesRating);
            SeriesRatingrepository.Save();
            return new GeneralResponse() { IsSuccess = true, Data = addRatingDTO };
        }

        [HttpDelete("{id}")]
        public ActionResult<GeneralResponse> DeleteRating(int id)
        {
            var existingRating = SeriesRatingrepository.GetById(id);

            if (existingRating == null)
            {
                return new GeneralResponse() { IsSuccess = false, Data = $"Rating Not Found With ID: {id}"};
            }

            SeriesRatingrepository.Delete(existingRating.Id);
            SeriesRatingrepository.Save();

            return new GeneralResponse() { IsSuccess = true, Data = $"Rating with id {id} has been deleted." };
        }
    }
}
