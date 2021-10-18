using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class Update
    {
        private MainForm _updateForm;

        public Update(MainForm form)
        {
            _updateForm = form;
            _updateForm.SetUpdateController(this);
        }
    }
}
