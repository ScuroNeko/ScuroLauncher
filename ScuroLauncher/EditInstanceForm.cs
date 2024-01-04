using ScuroLauncher.Settings;

namespace ScuroLauncher;

public partial class EditInstanceForm : Form
{
    private MainForm _mainForm;
    private InstanceItem _instance;

    public EditInstanceForm(MainForm mainForm, InstanceItem instance)
    {
        _mainForm = mainForm;
        _instance = instance;
        InitializeComponent();

        InstanceImage.Image = Utils.LoadImageForInstance(instance);
        InstanceName.Text = instance.Name;
    }

    private void Save_Click(object sender, EventArgs e)
    {
        Utils.UpdateInstance(_instance);
    }

    private void InstanceName_TextChanged(object sender, EventArgs e)
    {
        _instance.Name = InstanceName.Text;
    }
}
