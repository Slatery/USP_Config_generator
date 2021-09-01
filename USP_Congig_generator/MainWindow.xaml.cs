using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace USP_Congig_generator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static class Statics
        {
            public static String[] RS = { "x", "RS", "RS2" };
            public static String[] CU = { "x", "CU" };
            public static String[] KP = { "x", "KP" };
            public static String[] GL = { "x", "MX", "MX2", "MX3", "MX4", "MX5", "MX6", "MX7", "OR", "OR2", "OR3", "OR4", "OR5", "OR6" };
            public static String[] GL_TEX = { "x", "\\usp_gear_body\\model\\tx\\usp_mechanix_blk_co.paa", "\\usp_gear_body\\model\\tx\\usp_mechanix_blk2_co.paa", "\\usp_gear_body\\model\\tx\\usp_mechanix_tan_co.paa", "\\usp_gear_body\\model\\tx\\usp_mechanix_wdl_co.paa", "\\usp_gear_body\\model\\tx\\usp_mechanix_mc_co.paa", "\\usp_gear_body\\model\\tx\\usp_mechanix_mcb_co.paa", "\\usp_gear_body\\model\\tx\\usp_mechanix_gry_co.paa", "\\usp_gear_body\\model\\tx\\usp_overlord_blk_co.paa", "\\usp_gear_body\\model\\tx\\usp_overlord_cbr_co.paa", "\\usp_gear_body\\model\\tx\\usp_overlord_grn_co.paa", "\\usp_gear_body\\model\\tx\\usp_overlord_mc_co.paa", "\\usp_gear_body\\model\\tx\\usp_overlord_mix_co.paa", "\\usp_gear_body\\model\\tx\\usp_overlord_tan_co.paa" };

        }
        struct ClassResponse
        {
            public List<String> CLASS_RESPONSE;
            public List<String> TEXTURE_RESPONSE;
        }
        private void Clipboard_Weapons_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Output_cfgweapons.Text);
        }
        private void Clipboard_Vehicles_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Output_cfgvehicles.Text);
        }

        // G3 Actions
        static ClassResponse Generate_G3_Classes()
        {
            List<String> CLASS_LIST = new List<string>();
            List<String> TEXTURE_LIST = new List<string>();
            foreach (string i in Statics.RS)
            {
                String temp_outputa = "USP_G3C";
                if (i != "x")
                {
                    temp_outputa = temp_outputa + "_" + i;
                }
                foreach (string j in Statics.CU)
                {
                    String temp_outputb = temp_outputa;
                    if (j != "x")
                    {
                        temp_outputb = temp_outputb + "_" + j;
                    }
                    foreach (string k in Statics.KP)
                    {
                        String temp_outputc = temp_outputb;
                        if (k != "x")
                        {
                            temp_outputc = temp_outputc + "_" + k;
                        }
                        for (int m = 0; m < Statics.GL.Length; m++)
                        {
                            String temp_outputd = temp_outputc;
                            if (Statics.GL[m] != "x")
                            {
                                temp_outputd = temp_outputd + "_" + Statics.GL[m];
                            }

                            if (!CLASS_LIST.Contains(temp_outputd))
                            {
                                TEXTURE_LIST.Add(Statics.GL_TEX[m]);
                                CLASS_LIST.Add(temp_outputd);
                            }
                        }
                        if (!CLASS_LIST.Contains(temp_outputc))
                        {
                            TEXTURE_LIST.Add("x");
                            CLASS_LIST.Add(temp_outputc);
                        }
                    }
                    if (!CLASS_LIST.Contains(temp_outputb))
                    {
                        TEXTURE_LIST.Add("x");
                        CLASS_LIST.Add(temp_outputb);
                    }
                }
                if (!CLASS_LIST.Contains(temp_outputa))
                {
                    TEXTURE_LIST.Add("x");
                    CLASS_LIST.Add(temp_outputa);
                }
            }
            ClassResponse RESPONSE = new ClassResponse();
            RESPONSE.CLASS_RESPONSE = CLASS_LIST;
            RESPONSE.TEXTURE_RESPONSE = TEXTURE_LIST;
            return RESPONSE;
        }
        public String Generate_G3C_Weapon_Classes(List<string> CLASS_LIST, String SHIRT_TEXTURE, String PANT_TEXTURE, String EXTENSION, String NAME, String INV_IMAGE)
        {
            String[] CLASS_ARR = CLASS_LIST.ToArray();
            String RESPONSE = "";
            String EXTRAS = " ";
            foreach (string i in CLASS_ARR)
            {

                String[] CLASS_COMPS = i.Split('_');
                String CLASS_NAME = "";
                if (Parent_Classes.IsChecked == true && !CLASS_COMPS.Contains("MX2") && !CLASS_COMPS.Contains("MX3") && !CLASS_COMPS.Contains("MX4") && !CLASS_COMPS.Contains("MX5") && !CLASS_COMPS.Contains("MX6") && !CLASS_COMPS.Contains("MX7") && !CLASS_COMPS.Contains("OR2") && !CLASS_COMPS.Contains("OR3") && !CLASS_COMPS.Contains("OR4") && !CLASS_COMPS.Contains("OR5") && !CLASS_COMPS.Contains("OR6"))
                {
                    CLASS_NAME = String.Join("_", CLASS_COMPS);
                    RESPONSE = RESPONSE + "class " + CLASS_NAME + ";\n";
                }
            }
            foreach (string i in CLASS_ARR)
            {
                String[] CLASS_COMPS = i.Split('_');

                String PARENT_CLASS = i;
                CLASS_COMPS = CLASS_COMPS.Where(val => val != "USP").ToArray();
                CLASS_COMPS = CLASS_COMPS.Where(val => val != "G3C").ToArray();

                EXTRAS = String.Join("/", CLASS_COMPS);
                if (EXTRAS != "")
                {
                    EXTRAS = " " + EXTRAS + " ";
                }
                else
                {
                    EXTRAS = " ";
                }

                if (CLASS_COMPS.Contains("MX2") || CLASS_COMPS.Contains("MX3") || CLASS_COMPS.Contains("MX4") || CLASS_COMPS.Contains("MX5") || CLASS_COMPS.Contains("MX6") || CLASS_COMPS.Contains("MX7") || CLASS_COMPS.Contains("OR2") || CLASS_COMPS.Contains("OR3") || CLASS_COMPS.Contains("OR4") || CLASS_COMPS.Contains("OR5") || CLASS_COMPS.Contains("OR6"))
                {
                    PARENT_CLASS = PARENT_CLASS.Remove(PARENT_CLASS.Length - 1);
                }
                RESPONSE = RESPONSE + "class " + i + EXTENSION + ": " + PARENT_CLASS + "{\n";
                RESPONSE = RESPONSE + "    author = \"UnderSiege Productions\";\n";
                RESPONSE = RESPONSE + "    scope = 2;\n";
                RESPONSE = RESPONSE + "    scopeArsenal = 2;\n";
                RESPONSE = RESPONSE + "    displayName = \"[SOCOMD] Crye G3C" + EXTRAS + "(" + NAME + ")\";\n";
                RESPONSE = RESPONSE + "    picture = \"" + INV_IMAGE + "\";\n";
                RESPONSE = RESPONSE + "    hiddenSelectionsTextures[] = { \"" + SHIRT_TEXTURE + "\", \"" + PANT_TEXTURE + "\" };\n";
                RESPONSE = RESPONSE + "    class ItemInfo : UniformItem\n";
                RESPONSE = RESPONSE + "    {\n";
                RESPONSE = RESPONSE + "        uniformModel = \"-\";\n";
                RESPONSE = RESPONSE + "        uniformClass = \"" + i + EXTENSION + "\";\n";
                RESPONSE = RESPONSE + "        containerClass = \"Supply80\";\n";
                RESPONSE = RESPONSE + "        mass = 40;\n";

                RESPONSE = RESPONSE + "    };\n";
                RESPONSE = RESPONSE + "};\n";
            }
            return RESPONSE;
        }
        public String Generate_G3C_Vehicle_Classes(List<string> CLASS_LIST, List<string> TEXTURE_LIST, String SHIRT_TEXTURE, String PANT_TEXTURE, String SHOE_TEXTURE, String FLAG_LEFT, String FLAG_RIGHT, String EXTENSION, String NAME, String INV_IMAGE)
        {
            String[] CLASS_ARR = CLASS_LIST.ToArray();
            String[] TEXTURE_ARR = TEXTURE_LIST.ToArray();
            String RESPONSE = "";
            String EXTRAS = " ";
            if (Parent_Classes.IsChecked == true)
            {

                foreach (string i in CLASS_ARR)
                {

                    String[] CLASS_COMPS = i.Split('_');
                    String CLASS_NAME = "";
                    if (!CLASS_COMPS.Contains("MX2") && !CLASS_COMPS.Contains("MX3") && !CLASS_COMPS.Contains("MX4") && !CLASS_COMPS.Contains("MX5") && !CLASS_COMPS.Contains("MX6") && !CLASS_COMPS.Contains("MX7") && !CLASS_COMPS.Contains("OR2") && !CLASS_COMPS.Contains("OR3") && !CLASS_COMPS.Contains("OR4") && !CLASS_COMPS.Contains("OR5") && !CLASS_COMPS.Contains("OR6"))
                    {
                        CLASS_NAME = String.Join("_", CLASS_COMPS);
                        RESPONSE = RESPONSE + "class " + CLASS_NAME + ";\n";
                    }
                }
            }
            for (int i = 0; i < CLASS_ARR.Length; i++)
            {
                String GLOVES_TEXTURE = "";
                String[] CLASS_COMPS = CLASS_ARR[i].Split('_');
                String PARENT_CLASS = CLASS_ARR[i];
                CLASS_COMPS = CLASS_COMPS.Where(val => val != "USP").ToArray();
                CLASS_COMPS = CLASS_COMPS.Where(val => val != "G3C").ToArray();
                EXTRAS = String.Join("/", CLASS_COMPS);
                if (EXTRAS != "")
                {
                    EXTRAS = " " + EXTRAS + " ";
                }
                else
                {
                    EXTRAS = " ";
                }
                if (TEXTURE_ARR[i] != "x")
                {
                    GLOVES_TEXTURE = ",\"" + TEXTURE_ARR[i] + "\"";
                }
                if (CLASS_COMPS.Contains("MX2") || CLASS_COMPS.Contains("MX3") || CLASS_COMPS.Contains("MX4") || CLASS_COMPS.Contains("MX5") || CLASS_COMPS.Contains("MX6") || CLASS_COMPS.Contains("MX7") || CLASS_COMPS.Contains("OR2") || CLASS_COMPS.Contains("OR3") || CLASS_COMPS.Contains("OR4") || CLASS_COMPS.Contains("OR5") || CLASS_COMPS.Contains("OR6"))
                {
                    PARENT_CLASS = PARENT_CLASS.Remove(PARENT_CLASS.Length - 1);
                }
                RESPONSE = RESPONSE + "class " + CLASS_ARR[i] + EXTENSION + ": " + PARENT_CLASS + "{\n";
                RESPONSE = RESPONSE + "    author = \"UnderSiege Productions\";\n";
                RESPONSE = RESPONSE + "    displayName = \"[SOCOMD] Crye G3C" + EXTRAS + "(" + NAME + ")\";\n";
                RESPONSE = RESPONSE + "    uniformClass = \"" + CLASS_ARR[i] + EXTENSION + "\";\n";
                RESPONSE = RESPONSE + "    picture = \"" + INV_IMAGE + "\";\n";
                RESPONSE = RESPONSE + "    hiddenSelectionsTextures[] = {\"" + SHIRT_TEXTURE + "\",\"" + PANT_TEXTURE + "\"" + GLOVES_TEXTURE + ",\"" + SHOE_TEXTURE + "\",\"\",\"\",\"\",\"\",\"" + FLAG_LEFT + "\",\"" + FLAG_RIGHT + "\",\"\",\"\",\"\"};\n";
                RESPONSE = RESPONSE + "};\n";
            }
            return RESPONSE;
        }


        // Tshirt
        static ClassResponse Generate_Other_Classes(String PREFIX)
        {
            List<String> CLASS_LIST = new List<string>();
            List<String> TEXTURE_LIST = new List<string>();
            
            foreach (string k in Statics.KP)
            {
                String temp_outputa = PREFIX;
                if (k != "x")
                {
                    temp_outputa = temp_outputa + "_" + k;
                }
                for (int m = 0; m < Statics.GL.Length; m++)
                {
                    String temp_outputb = temp_outputa;
                    if (Statics.GL[m] != "x")
                    {
                        temp_outputb = temp_outputb + "_" + Statics.GL[m];
                    }

                    if (!CLASS_LIST.Contains(temp_outputb))
                    {
                        TEXTURE_LIST.Add(Statics.GL_TEX[m]);
                        CLASS_LIST.Add(temp_outputb);
                    }
                }
                if (!CLASS_LIST.Contains(temp_outputa))
                {
                    TEXTURE_LIST.Add("x");
                    CLASS_LIST.Add(temp_outputa);
                }
            }
            ClassResponse RESPONSE = new ClassResponse();
            RESPONSE.CLASS_RESPONSE = CLASS_LIST;
            RESPONSE.TEXTURE_RESPONSE = TEXTURE_LIST;
            return RESPONSE;
        }
        public String Generate_Tshirt_Weapon_Classes(List<string> CLASS_LIST, String SHIRT_TEXTURE, String PANT_TEXTURE, String EXTENSION, String NAME, String INV_IMAGE, String SHIRT_TEXT)
        {
            String[] CLASS_ARR = CLASS_LIST.ToArray();
            String RESPONSE = "";
            String EXTRAS = " ";
            foreach (string i in CLASS_ARR)
            {

                String[] CLASS_COMPS = i.Split('_');
                String CLASS_NAME = "";
                if(Parent_Classes.IsChecked == true && !CLASS_COMPS.Contains("MX2") && !CLASS_COMPS.Contains("MX3") && !CLASS_COMPS.Contains("MX4") && !CLASS_COMPS.Contains("MX5") && !CLASS_COMPS.Contains("MX6") && !CLASS_COMPS.Contains("MX7") && !CLASS_COMPS.Contains("OR2") && !CLASS_COMPS.Contains("OR3") && !CLASS_COMPS.Contains("OR4") && !CLASS_COMPS.Contains("OR5") && !CLASS_COMPS.Contains("OR6"))
                {
                    CLASS_NAME = String.Join("_", CLASS_COMPS);
                    RESPONSE = RESPONSE + "class " + CLASS_NAME + ";\n";
                }
            }
            foreach (string i in CLASS_ARR)
            {
                String[] CLASS_COMPS = i.Split('_');
                String PARENT_CLASS = i;
                CLASS_COMPS = CLASS_COMPS.Where(val => val != "USP").ToArray();
                CLASS_COMPS = CLASS_COMPS.Where(val => val != "G3C").ToArray();
                EXTRAS = String.Join("/", CLASS_COMPS);
                if (EXTRAS != "")
                {
                    EXTRAS = " " + EXTRAS + " ";
                }
                else
                {
                    EXTRAS = " ";
                }
                if (CLASS_COMPS.Contains("MX2") || CLASS_COMPS.Contains("MX3") || CLASS_COMPS.Contains("MX4") || CLASS_COMPS.Contains("MX5") || CLASS_COMPS.Contains("MX6") || CLASS_COMPS.Contains("MX7") || CLASS_COMPS.Contains("OR2") || CLASS_COMPS.Contains("OR3") || CLASS_COMPS.Contains("OR4") || CLASS_COMPS.Contains("OR5") || CLASS_COMPS.Contains("OR6"))
                {
                    PARENT_CLASS = PARENT_CLASS.Remove(PARENT_CLASS.Length - 1);
                }
                RESPONSE = RESPONSE + "class " + i + EXTENSION + ": " + PARENT_CLASS + "{\n";
                RESPONSE = RESPONSE + "    author = \"UnderSiege Productions\";\n";
                RESPONSE = RESPONSE + "    scope = 2;\n";
                RESPONSE = RESPONSE + "    scopeArsenal = 2;\n";
                RESPONSE = RESPONSE + "    displayName = \"[SOCOMD] Crye " + SHIRT_TEXT + "/G3C" + EXTRAS + "(" + NAME + ")\";\n";
                RESPONSE = RESPONSE + "    picture = \"" + INV_IMAGE + "\";\n";
                RESPONSE = RESPONSE + "    hiddenSelectionsTextures[] = { \"" + SHIRT_TEXTURE + "\", \"" + PANT_TEXTURE + "\" };\n";
                RESPONSE = RESPONSE + "    class ItemInfo : UniformItem\n";
                RESPONSE = RESPONSE + "    {\n";
                RESPONSE = RESPONSE + "        uniformModel = \"-\";\n";
                RESPONSE = RESPONSE + "        uniformClass = \"" + i + EXTENSION + "\";\n";
                RESPONSE = RESPONSE + "        containerClass = \"Supply20\";\n";
                RESPONSE = RESPONSE + "        mass = 80;\n";

                RESPONSE = RESPONSE + "    };\n";
                RESPONSE = RESPONSE + "};\n";
            }
            return RESPONSE;
        }
        public String Generate_Tshirt_Vehicle_Classes(List<string> CLASS_LIST, List<string> TEXTURE_LIST, String SHIRT_TEXTURE, String PANT_TEXTURE, String SHOE_TEXTURE, String EXTENSION, String NAME, String INV_IMAGE, String SHIRT_TEXT)
        {
            String[] CLASS_ARR = CLASS_LIST.ToArray();
            String[] TEXTURE_ARR = TEXTURE_LIST.ToArray();
            String RESPONSE = "";
            String EXTRAS = " ";
            foreach (string i in CLASS_ARR)
            {

                String[] CLASS_COMPS = i.Split('_');
                String CLASS_NAME = "";
                if (Parent_Classes.IsChecked == true && !CLASS_COMPS.Contains("MX2") && !CLASS_COMPS.Contains("MX3") && !CLASS_COMPS.Contains("MX4") && !CLASS_COMPS.Contains("MX5") && !CLASS_COMPS.Contains("MX6") && !CLASS_COMPS.Contains("MX7") && !CLASS_COMPS.Contains("OR2") && !CLASS_COMPS.Contains("OR3") && !CLASS_COMPS.Contains("OR4") && !CLASS_COMPS.Contains("OR5") && !CLASS_COMPS.Contains("OR6"))
                {
                    CLASS_NAME = String.Join("_", CLASS_COMPS);
                    RESPONSE = RESPONSE + "class " + CLASS_NAME + ";\n";
                }
            }
            for (int i = 0; i < CLASS_ARR.Length; i++)
            {
                String GLOVES_TEXTURE = "";
                String PARENT_CLASS = CLASS_ARR[i];
                String[] CLASS_COMPS = CLASS_ARR[i].Split('_');
                CLASS_COMPS = CLASS_COMPS.Where(val => val != "USP").ToArray();
                CLASS_COMPS = CLASS_COMPS.Where(val => val != "G3C").ToArray();
                EXTRAS = String.Join("/", CLASS_COMPS);
                if (EXTRAS != "")
                {
                    EXTRAS = " " + EXTRAS + " ";
                }
                else
                {
                    EXTRAS = " ";
                }
                if (TEXTURE_ARR[i] != "x")
                {
                    GLOVES_TEXTURE = ",\"" + TEXTURE_ARR[i] + "\"";
                }
                if (CLASS_COMPS.Contains("MX2") || CLASS_COMPS.Contains("MX3") || CLASS_COMPS.Contains("MX4") || CLASS_COMPS.Contains("MX5") || CLASS_COMPS.Contains("MX6") || CLASS_COMPS.Contains("MX7") || CLASS_COMPS.Contains("OR2") || CLASS_COMPS.Contains("OR3") || CLASS_COMPS.Contains("OR4") || CLASS_COMPS.Contains("OR5") || CLASS_COMPS.Contains("OR6"))
                {
                    PARENT_CLASS = PARENT_CLASS.Remove(PARENT_CLASS.Length - 1);
                }
                RESPONSE = RESPONSE + "class " + CLASS_ARR[i] + EXTENSION + ": " + PARENT_CLASS + "{\n";
                RESPONSE = RESPONSE + "    author = \"UnderSiege Productions\";\n";
                RESPONSE = RESPONSE + "    displayName = \"[SOCOMD] Crye " + SHIRT_TEXT + "/G3C" + EXTRAS + "(" + NAME + ")\";\n";
                RESPONSE = RESPONSE + "    uniformClass = \"" + CLASS_ARR[i] + EXTENSION + "\";\n";
                RESPONSE = RESPONSE + "    picture = \"" + INV_IMAGE + "\";\n";
                RESPONSE = RESPONSE + "    hiddenSelectionsTextures[] = {\"" + SHIRT_TEXTURE + "\",\"" + PANT_TEXTURE + "\"" + GLOVES_TEXTURE + ",\"" + SHOE_TEXTURE + "\"};\n";
                RESPONSE = RESPONSE + "};\n";
            }
            return RESPONSE;
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            String EXTENSION = Class_Extension.Text;
            String NAME = Display_Name.Text;
            String SHIRT_PATH = Uniform_Shirt.Text;
            String PANT_PATH = Uniform_Pant.Text;
            String UNIFORM_SET = Uniform_Set.Text;
            String UNIFORM_SHOE = Uniform_Shoe.Text;
            String L_FLAG_PATH = Flag_Left.Text;
            String R_FLAG_PATH = Flag_Right.Text;
            String INV_IMAGE = Inventory_Icon.Text;
            List<String> CLASS_LIST = new List<string>();
            List<String> TEXTURE_LIST = new List<string>();
            String WEAPONS_OUTPUT = "";
            String VEHICLES_OUTPUT = "";
            if (UNIFORM_SET == "G3C Set")
            {
                ClassResponse CLASS_RESPONSE = Generate_G3_Classes();
                CLASS_LIST = CLASS_RESPONSE.CLASS_RESPONSE;
                TEXTURE_LIST = CLASS_RESPONSE.TEXTURE_RESPONSE;
                WEAPONS_OUTPUT = Generate_G3C_Weapon_Classes(CLASS_LIST, SHIRT_PATH, PANT_PATH, EXTENSION, NAME, INV_IMAGE);
                VEHICLES_OUTPUT = Generate_G3C_Vehicle_Classes(CLASS_LIST, TEXTURE_LIST, SHIRT_PATH, PANT_PATH, UNIFORM_SHOE, L_FLAG_PATH, R_FLAG_PATH, EXTENSION, NAME, INV_IMAGE);
            }
            if (UNIFORM_SET == "T-Shirt")
            {
                ClassResponse CLASS_RESPONSE = Generate_Other_Classes("USP_TSHIRT_G3C");
                CLASS_LIST = CLASS_RESPONSE.CLASS_RESPONSE;
                TEXTURE_LIST = CLASS_RESPONSE.TEXTURE_RESPONSE;
                WEAPONS_OUTPUT = Generate_Tshirt_Weapon_Classes(CLASS_LIST, SHIRT_PATH, PANT_PATH, EXTENSION, NAME, INV_IMAGE, "T-Shirt");
                VEHICLES_OUTPUT = Generate_Tshirt_Vehicle_Classes(CLASS_LIST, TEXTURE_LIST, SHIRT_PATH, PANT_PATH, UNIFORM_SHOE, EXTENSION, NAME, INV_IMAGE, "T-Shirt");
            }

            if (UNIFORM_SET == "Soft Shell")
            {
                ClassResponse CLASS_RESPONSE = Generate_Other_Classes("USP_SOFTSHELL_G3C");
                CLASS_LIST = CLASS_RESPONSE.CLASS_RESPONSE;
                TEXTURE_LIST = CLASS_RESPONSE.TEXTURE_RESPONSE;
                WEAPONS_OUTPUT = Generate_Tshirt_Weapon_Classes(CLASS_LIST, SHIRT_PATH, PANT_PATH, EXTENSION, NAME, INV_IMAGE, "Soft Shell");
                VEHICLES_OUTPUT = Generate_Tshirt_Vehicle_Classes(CLASS_LIST, TEXTURE_LIST, SHIRT_PATH, PANT_PATH, UNIFORM_SHOE, EXTENSION, NAME, INV_IMAGE, "Soft Shell");
            }

            if (UNIFORM_SET == "PCU")
            {
                ClassResponse CLASS_RESPONSE = Generate_Other_Classes("USP_PCU_G3C");
                CLASS_LIST = CLASS_RESPONSE.CLASS_RESPONSE;
                TEXTURE_LIST = CLASS_RESPONSE.TEXTURE_RESPONSE;
                WEAPONS_OUTPUT = Generate_Tshirt_Weapon_Classes(CLASS_LIST, SHIRT_PATH, PANT_PATH, EXTENSION, NAME, INV_IMAGE, "PCU");
                VEHICLES_OUTPUT = Generate_Tshirt_Vehicle_Classes(CLASS_LIST, TEXTURE_LIST, SHIRT_PATH, PANT_PATH, UNIFORM_SHOE, EXTENSION, NAME, INV_IMAGE, "PCU");
            }

            if (UNIFORM_SET == "Overwhite")
            {
                ClassResponse CLASS_RESPONSE = Generate_Other_Classes("USP_OVERWHITE_G3C");
                CLASS_LIST = CLASS_RESPONSE.CLASS_RESPONSE;
                TEXTURE_LIST = CLASS_RESPONSE.TEXTURE_RESPONSE;
                WEAPONS_OUTPUT = Generate_Tshirt_Weapon_Classes(CLASS_LIST, SHIRT_PATH, PANT_PATH, EXTENSION, NAME, INV_IMAGE, "Overwhite");
                VEHICLES_OUTPUT = Generate_Tshirt_Vehicle_Classes(CLASS_LIST, TEXTURE_LIST, SHIRT_PATH, PANT_PATH, UNIFORM_SHOE, EXTENSION, NAME, INV_IMAGE, "Overwhite");
            }
            Output_cfgweapons.Text = WEAPONS_OUTPUT;
            Output_cfgvehicles.Text = VEHICLES_OUTPUT;
        }

        private void Uniform_Set_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Uniform_Shirt != null)
            {
                int selectedIndex = Uniform_Set.SelectedIndex;
                if(selectedIndex != 4)
                {
                    Uniform_Shirt.IsEnabled = true;
                }
                switch (selectedIndex)
                {
                    case 0:
                        Uniform_Shirt.Text = "\\usp_gear_body\\model\\tx\\usp_g3c_shirt_mc_co.paa"; break;
                    case 1:
                        Uniform_Shirt.Text = "\\usp_gear_body\\model\\tx\\usp_tshirt_nav_co.paa"; break;
                    case 2:
                        Uniform_Shirt.Text = "\\usp_gear_body\\model\\tx\\usp_pcu_gry_co.paa"; break;
                    case 3:
                        Uniform_Shirt.Text = "\\usp_gear_body\\model\\tx\\usp_softshell_mc_co.paa"; break;
                    case 4:
                        Uniform_Shirt.Text = ""; Uniform_Shirt.IsEnabled = false; break;

                }
            }
        }
        
    }
}
