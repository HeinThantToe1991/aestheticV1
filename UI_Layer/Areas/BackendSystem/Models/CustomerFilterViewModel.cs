using UI_Layer.Models;

namespace UI_Layer.Areas.BackendSystem.Models
{
    public class CustomerFilterViewModel : BaseEntityViewModel
    {
        public string FilterKey { get; set; }
        public string FilterFromValue { get; set; }
        public string FilterToValue { get; set; }
        public string Filter1Key { get; set; }
        public string Filter1Value1 { get; set; }

        public string FilterType { get; set; }

        public string FilterEquation { get; set;}

    }


}
