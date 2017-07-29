using System.Threading.Tasks;
using App3.Models;

namespace App3.Services
{
    public interface IBingSpeechService
    {
        Task<SpeechResult> RecognizeSpeechAsync(string filename);
    }
}