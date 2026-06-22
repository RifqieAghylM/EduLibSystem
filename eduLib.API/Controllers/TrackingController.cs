using Microsoft.AspNetCore.Mvc;
using eduLib.Application.Tracking;
using eduLib.Infrastructure.Storage;

namespace eduLib.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingController : ControllerBase
    {
        private readonly BookmarkManager _bookmarkManager;
        private readonly ReadingStateMachine _readingStateMachine;
        private readonly MongoTrackingRepository _trackingRepo;

        public TrackingController(BookmarkManager bookmarkManager,
                                  ReadingStateMachine readingStateMachine,
                                  MongoTrackingRepository trackingRepo)
        {
            _bookmarkManager = bookmarkManager;
            _readingStateMachine = readingStateMachine;
            _trackingRepo = trackingRepo;
        }

        // POST: api/Tracking/bookmark
        [HttpPost("bookmark")]
        public async Task<IActionResult> SaveBookmark(
            [FromQuery] string bookId,
            [FromQuery] int page)
        {
            // 1. Logika table-driven di memori
            _bookmarkManager.SaveBookmark(bookId, page);
            // 2. Simpan ke MongoDB
            await _trackingRepo.SaveBookmarkAsync(bookId, page);

            return Ok("Bookmark berhasil disimpan.");
        }

        // GET: api/Tracking/bookmark
        [HttpGet("bookmark")]
        public async Task<IActionResult> GetBookmark([FromQuery] string bookId)
        {
            var page = await _trackingRepo.GetBookmarkAsync(bookId);
            return Ok(new { bookId, bookmarkedPage = page });
        }

        // POST: api/Tracking/reading-progress
        [HttpPost("reading-progress")]
        public async Task<IActionResult> UpdateProgress(
            [FromQuery] string bookId,
            [FromQuery] int currentPage,
            [FromQuery] int totalPage)
        {
            // 1. Logika automata
            _readingStateMachine.UpdateProgress(currentPage, totalPage);
            // 2. Simpan ke MongoDB
            await _trackingRepo.SaveProgressAsync(
                bookId, currentPage, totalPage,
                _readingStateMachine.CurrentState.ToString());

            return Ok(new { bookId, state = _readingStateMachine.CurrentState.ToString() });
        }

        // GET: api/Tracking/reading-progress
        [HttpGet("reading-progress")]
        public async Task<IActionResult> GetProgress([FromQuery] string bookId)
        {
            var progress = await _trackingRepo.GetProgressAsync(bookId);
            if (progress == null)
                return NotFound("Progress tidak ditemukan.");

            return Ok(new
            {
                bookId,
                currentPage = progress.CurrentPage,
                totalPages = progress.TotalPages,
                state = progress.ReadingState
            });
        }
    }
}