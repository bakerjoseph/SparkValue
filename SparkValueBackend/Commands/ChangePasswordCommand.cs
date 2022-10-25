using SparkValueBackend.Services;

namespace SparkValueBackend.Commands
{
    public class ChangePasswordCommand : CommandBase
    {
        private readonly NavigationService _userSettingsViewNavigationService;

        public ChangePasswordCommand(NavigationService userSettingsViewNavigationService)
        {
            _userSettingsViewNavigationService = userSettingsViewNavigationService;
        }

        public override void Execute(object? parameter)
        {
            // Change a user's password here!!

            _userSettingsViewNavigationService.Navigate();
        }
    }
}
