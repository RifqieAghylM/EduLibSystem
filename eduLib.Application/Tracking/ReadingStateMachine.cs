using System;
using eduLib.Core.Enums;

namespace eduLib.Application.Tracking
{
    
    public class ReadingStateMachine
    {
        public ReadingState CurrentState { get; private set; } = ReadingState.NotStarted;

        public void UpdateProgress(int currentPage, int totalPages)
        {
            // defensive Programming 
            if (totalPages <= 0)
                throw new ArgumentException("Total halaman harus lebih dari 0.");
            if (currentPage < 0 || currentPage > totalPages)
                throw new ArgumentException("Halaman saat ini tidak valid.");

            // logika Transisi Automata
            if (currentPage == 0)
            {
                CurrentState = ReadingState.NotStarted;
            }
            else if (currentPage < totalPages)
            {
                CurrentState = ReadingState.Reading;
            }
            else
            {
                CurrentState = ReadingState.Completed;
            }
        }
    }
}