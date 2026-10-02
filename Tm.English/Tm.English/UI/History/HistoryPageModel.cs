using Tm.English.Data;
using Tm.English.Data.Domain;
using Tm.Mobile.Core;

namespace Tm.English.UI
{
    public class HistoryPageModel : PageViewModel
    {
        readonly IDataRepository repository;

        public HistoryPageModel(IDataRepository repository)
        {
            Title = "History";

            this.repository = repository;

            LoadData();
        }

        public async void LoadData()
        {
            List.AddRange(await repository.GetCodes());
        }
        
        RangeObservableCollection<Code> list;
        public RangeObservableCollection<Code> List
        {
            get => list ?? (list = new RangeObservableCollection<Code>());
        }
    }
}
