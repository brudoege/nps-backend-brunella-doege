using nps_backend_brunella_doege.Domain.Enums;

namespace nps_backend_brunella_doege.Application.ViewModels
{
    public class NpsResultManipulacaoViewModel
    {
        public CategoryType Category { get; set; }
        public string Comments { get; set; }
        public int Score { get; set; }
        public string UserId { get; set; }
    }
}
