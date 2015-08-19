using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SwitcherCommon;
using ConfigurationImpl = SwitcherUi.config.ConfigurationImpl;

namespace SwitcherUi.switching.cfg
{
    public partial class FrmDatabaseServices : Form
    {
        private class DatabaseServices
        {
            public string Name { get; set; }
            public WindowsServiceDescription[] Services { get; set; }
        }

        private readonly DatabaseControllerConfig _config;

        private readonly Dictionary<string, WindowsServiceDescription> _services;
        private readonly List<DatabaseServices> _databases;

        private WindowsServiceDescription WindowsService(string name)
        {
            if (_services.ContainsKey(name)) return _services[name];
            return new WindowsServiceDescription
            {
                Name = name,
                DisplayName = $"*** UNKNOWN SERVICE: {name}"
            };
        }

        private WindowsServiceDescription[] WindowsServices(string[] names)
        {
            return names.Select(WindowsService).ToArray();
        }


        public FrmDatabaseServices(config.IConfiguration config)
        {
            InitializeComponent();
            _config =  new DatabaseControllerConfig(config);
            _services = WindowsServiceDescription.Load(config).ToDictionary(s => s.Name, s => s, StringComparer.InvariantCultureIgnoreCase);
            _databases = _config.DatabaseAndServices.Select(d => new DatabaseServices {Name = d.Key, Services = WindowsServices(d.Value)}).ToList();
            edtTimeoutSeconds.Value = _config.Timeout;
        }


        public FrmDatabaseServices(): this(new ConfigurationImpl(new CommandArguments(new string[0])))
        {
        }

        private ListViewItem DatabaseListViewItem(DatabaseServices db)
        {
            var result = new ListViewItem {};
            result.Text = db.Name;
            result.Tag = db;
            result.SubItems.Add(string.Join("; ", db.Services.Select(s => s.DisplayName)));
            return result;
        }


        private readonly WindowsServiceDescription[] _emptyServiceList = new WindowsServiceDescription[0];
        private void SetupServicesCombos(DatabaseServices selectedDatabase)
        {
            cbServices.Items.Clear();
            cbServices.Items.AddRange(_services.Values.Except(selectedDatabase?.Services ?? _emptyServiceList).OrderBy(c => c.DisplayName).Cast<object>().ToArray());
        }

        private void DisplayDatabases()
        {
            var selected = SelectedDatabase();
            lvDatabase.Items.Clear();
            lvDatabase.Items.AddRange(_databases.Select(DatabaseListViewItem).ToArray());
            (from ListViewItem item in lvDatabase.Items select item).Where(i => i.Tag == selected).ToList().ForEach(i => i.Selected = true);
        }

        private void FrmDatabaseServices_Load(object sender, EventArgs e)
        {
            DisplayDatabases();
            SetupServicesCombos(null);
            var args = new CommandArguments(Environment.GetCommandLineArgs());
            btnCopyServices.Visible = args.ArgumentExists("svc-copy");
        }

        private ListViewItem Listitem(WindowsServiceDescription serviceDesc)
        {
            var result = new ListViewItem {Text = serviceDesc.Name};
            result.SubItems.Add(serviceDesc.DisplayName);
            result.Tag = serviceDesc;
            return result;
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            var selected = (WindowsServiceDescription) cbServices.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show(this, "No Service Selected To Add", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lvServices.Items.Add(Listitem(selected));
            cbServices.Items.Remove(selected);
        }

        private List<ListViewItem> Selected(ListView listView)
        {
            return (from ListViewItem item in listView.SelectedItems select item).ToList();
        }


        private IEnumerable<T> Tags<T>(IEnumerable<ListViewItem> items)
        {
            return items.Select(i => i.Tag).Where(t => t != null).Cast<T>();
        }

        private IEnumerable<T> Tags<T>(ListView listView)
        {
            return Tags<T>((from ListViewItem item in listView.Items select item).ToList());
        }

        private void Remove(List<ListViewItem> items, ListView fromListView)
        {
            items.ForEach(i => fromListView.Items.Remove(i));
        }


        private void btnRemoveServices_Click(object sender, EventArgs e)
        {
            if (lvServices.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "No Services Selected To Remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var selected = Selected(lvServices);
            var services =  Tags<WindowsServiceDescription>(selected).OrderByDescending(s => s.DisplayName);
            cbServices.Items.AddRange(services.Cast<object>().ToArray());
            Remove(selected, lvServices);
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            var selected = Selected(lvServices);
            if (selected == null || selected.Count == 0)
            {
                MessageBox.Show(this, "No Service(s) Selected To Start Earlier", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var idx = Math.Max(0, selected.First().Index - 1);
            Remove(selected, lvServices);
            foreach (var item in selected)
            {
                lvServices.Items.Insert(idx++, item);
            }
        }

        private void btnDecrease_Click(object sender, EventArgs e)
        {
            var selected = Selected(lvServices);
            if (selected == null || selected.Count == 0)
            {
                MessageBox.Show(this, "No Service(s) Selected To Start Later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var idx = selected.Last().Index + 1;
            ListViewItem after = idx >= lvServices.Items.Count ? null : lvServices.Items[idx];
            Remove(selected, lvServices);
            idx = after?.Index + 1 ?? lvServices.Items.Count;
            foreach (var item in selected)
            {
                lvServices.Items.Insert(idx++, item);
            }
        }

        private bool HasChanged()
        {
            if (_editing == null)
            {
                return !string.IsNullOrWhiteSpace(edtDatabaseName.Text) || lvServices.Items.Count > 0;
            }
            return (!edtDatabaseName.Text.Equals(_editing.Name) ||
                    !_editing.Services.SequenceEqual(Tags<WindowsServiceDescription>(lvServices)));
        }

        private bool AllowChange()
        {
            return !HasChanged() || MessageBox.Show("Discard changes", "Discard changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private DatabaseServices SelectedDatabase()
        {
            if (lvDatabase.SelectedItems.Count == 0) return null;
            return (DatabaseServices)lvDatabase.SelectedItems[0].Tag;
        }

        private DatabaseServices _editing = null;

        private void SetEditing(DatabaseServices editing)
        {
            edtDatabaseName.Text = editing.Name;
            lvServices.Items.Clear();
            lvServices.Items.AddRange(editing.Services.Select(Listitem).ToArray());
            SetupServicesCombos(editing);
            _editing = editing;
            lblEditing.Text = $" (editing {_editing.Name}";
            btnAdd.Text = "Copy";
            btnUpdate.Enabled = true;
        }

        private void lvDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = SelectedDatabase();
            if (selected == null || !AllowChange()) return;
            SetEditing(selected);
        }

        private void Clear()
        {
            edtDatabaseName.Text = "";
            _editing = null;
            SetupServicesCombos(null);
            lvServices.Items.Clear();
            btnAdd.Text = "Add";
            btnUpdate.Enabled = false;
            lblEditing.Text = "(new)";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (!AllowChange()) return;
            Clear();
        }

        private bool IsNameOk(string name, bool skipEditing)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please provide a Name", "Empty Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var exclude = skipEditing && _editing != null ? new[] {_editing} : new DatabaseServices[0];
            if (_databases.Except(exclude).Any(d => name.Equals(d.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                MessageBox.Show($"There is already a database with Name '{name}", "Duplicate Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool AnyServices()
        {
            var result = lvServices.Items.Count > 0;
            if (!result)
            {
                MessageBox.Show($"No Services Selected", "Services", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var name = edtDatabaseName.Text.Trim().Replace("=", "_");
            if (!IsNameOk(name, false) || !AnyServices()) return;
            var newDb = new DatabaseServices
            {
                Name = name,
                Services = Tags<WindowsServiceDescription>(lvServices).ToArray()
            };
            _databases.Add(newDb);
            lvDatabase.Items.Add(DatabaseListViewItem(newDb));
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_editing == null)
            {
                btnAdd_Click(sender, e);
                return;
            }
            var name = edtDatabaseName.Text.Trim().Replace("=", "_");
            if (!IsNameOk(name, true) || !AnyServices()) return;
            _editing.Name = name;
            _editing.Services = Tags<WindowsServiceDescription>(lvServices).ToArray();
            DisplayDatabases();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!AllowChange()) return;
            var selected = SelectedDatabase();
            if (_editing != null && _editing != selected)
            {
                MessageBox.Show($"There is a Conflict between selected and Editing - please clear changes", "Conflict", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (selected == null)
            {
                MessageBox.Show($"Nothing selected to Delete", "Nothing selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (
                MessageBox.Show($"Delete Database '{selected.Name}'", "Delete Database", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _databases.Remove(selected);
                DisplayDatabases();
                Clear();
            }
        }

        private void btnClearChanges_Click(object sender, EventArgs e)
        {
            if (!AllowChange()) return;
            if (_editing == null)
            {
                Clear();
            }
            else
            {
                SetEditing(SelectedDatabase());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!AllowChange()) return;
            var resutlts = _databases.ToDictionary(d => d.Name, d => d.Services.Select(s => s.Name).ToArray());
            _config.SaveCfg(decimal.ToInt32(edtTimeoutSeconds.Value), resutlts);
            if (!Modal) Close();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!Modal) Close();
            DialogResult = DialogResult.Cancel;
        }

        private void btnCopyServices_Click(object sender, EventArgs e)
        {
            var svcsUsed = _databases.Select(c => c.Services).SelectMany(c => c).Distinct();
            var asStrs = _services.Values.Select(s => $"{s.Name}={s.DisplayName}");
            Clipboard.SetText(string.Join("\r\n", asStrs));
        }
    }
}
