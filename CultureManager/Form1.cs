using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CultureManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InstallCulture(string customCultureName, string customCultureTag, CultureInfo parentCultureInfo)
        {
            try
            {
                CultureAndRegionInfoBuilder.Unregister(customCultureTag);
            }
            catch (Exception) { }

            var cultureAndRegionInfoBuilder = new CultureAndRegionInfoBuilder(customCultureTag, CultureAndRegionModifiers.None);
            RegionInfo regionInfo = new RegionInfo(parentCultureInfo.Name);

            cultureAndRegionInfoBuilder.LoadDataFromCultureInfo(parentCultureInfo);
            cultureAndRegionInfoBuilder.LoadDataFromRegionInfo(regionInfo);
            cultureAndRegionInfoBuilder.IetfLanguageTag = customCultureTag;

            cultureAndRegionInfoBuilder.CultureEnglishName = customCultureName;
            cultureAndRegionInfoBuilder.CultureNativeName = customCultureName;

            //This code makes sure that we fallback to the parent culture on a missing translations
            //It will fallback to default culture if you remove this line
            cultureAndRegionInfoBuilder.Parent = parentCultureInfo;

            cultureAndRegionInfoBuilder.Register();
        }


        private void BuildCultureB_Click(object sender, EventArgs e)
        {
            try
            {
                CultureAndRegionInfoBuilder.Unregister("x-cdvsl");
            }
            catch (Exception) { }

            try
            {
                CultureAndRegionInfoBuilder.Unregister("x-CDORG");
            }
            catch (Exception) { }

            //InstallCulture("English (Vessel, CODie)", "x-cdvsl", "en-US");
            //InstallCulture("English (Org CODie)", "x-cdorg", "en-US");


            //CultureAndRegionInfoBuilder builder = new("x-cdvsl", CultureAndRegionModifiers.None);
            //builder.LoadDataFromCultureInfo(CultureInfo.GetCultureInfo("en-US"));
            //builder.IsMetric = true;
            //builder.IsRightToLeft = false;
            //builder.CultureNativeName = "CODie English Vessel";
            //builder.CultureEnglishName = "CODie English Vessel";
            //builder.IetfLanguageTag = "x-cdvsl";
            //builder.Register();


            //builder = new("x-CDORG", CultureAndRegionModifiers.Neutral);
            //builder.LoadDataFromCultureInfo(CultureInfo.GetCultureInfo("en-US"));
            //builder.IsMetric = true;
            //builder.IsRightToLeft = false;
            //builder.CultureNativeName = "CODie English Org";
            //builder.CultureEnglishName = "CODie English Org";
            //builder.IetfLanguageTag = "x-CDORG";
            //builder.Register();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ci = CultureInfo.GetCultureInfo("x-CDVSL");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var installedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            //foreach (var installedCulture in installedCultures)
            //{
            //}
        }

        private void RemoveB_Click(object sender, EventArgs e)
        {
            var ci = CultureInfo.GetCultureInfo($"x-{CultureAbbrevTB.Text.ToLower()}");
            CultureAndRegionInfoBuilder.Unregister(ci.Name);
        }

        private void RegisterB_Click(object sender, EventArgs e)
        {
            InstallCulture(CultureNameTB.Text, $"x-{CultureAbbrevTB.Text.ToLower()}", (CultureInfo)(cultureCB.SelectedItem));
        }

        private void LoadCulturesB_Click(object sender, EventArgs e)
        {
            cultureCB.Items.AddRange(CultureInfo.GetCultures(CultureTypes.AllCultures));
        }
    }
}
