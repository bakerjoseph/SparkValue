using SparkValueBackend.Services;

namespace SparkValueBackend.Commands
{
    public class ChangeUsernameCommand : AsyncCommandBase
    {
        private readonly NavigationService _userSettingsViewNavigationService;

        public ChangeUsernameCommand(NavigationService userSettingsViewNavigationService)
        {
            _userSettingsViewNavigationService = userSettingsViewNavigationService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {


            _userSettingsViewNavigationService.Navigate();
        }
    }
}
