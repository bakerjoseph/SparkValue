using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Stores
{
    public class EmailStatusStore
    {
        private bool _status;
        public bool Status
        {
            get { return _status; }
            set 
            { 
                _status = value; 
                OnEmailStatusChanged();
            }
        }

        public event Action EmailStatusChanged;

        private void OnEmailStatusChanged()
        {
            EmailStatusChanged?.Invoke();
        }
    }
}
