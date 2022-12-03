using SparkValueBackend.Services;

namespace SparkValueBackend.Commands
{
    public class ChangeUsernameCommand : CommandBase
    {
        private readonly NavigationService _userSettingsViewNavigationService;

        public ChangeUsernameCommand(NavigationService userSettingsViewNavigationService)
        {
            _userSettingsViewNavigationService = userSettingsViewNavigationService;
        }

        public override void Execute(object? parameter)
        {
            // Change a user's username here!!

            _userSettingsViewNavigationService.Navigate();
        }
    }
}
