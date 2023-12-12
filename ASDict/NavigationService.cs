using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASDict
{
    public class NavigationService
    {
        private static INavigation _navigation;

        public static void Initialize(INavigation navigation)
        {
            _navigation = navigation;
        }

        public static async Task NavigateToNewPage(Page page)
        {
            if (_navigation != null)
            {
                await _navigation.PushAsync(page);
            }
        }
    }
}
