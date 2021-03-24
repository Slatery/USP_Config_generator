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

        }
        private void Clipboard_Weapons_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Output_cfgweapons.Text);
        }
        private void Clipboard_Vehicles_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Output_cfgvehicles.Text);
        }
        static List<string> Generate_G3_Classes()
        {
            List<String> CLASS_LIST = new List<string>();
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
                        foreach (string l in Statics.GL)
                        {
                            String temp_outputd = temp_outputc;
                            if (l != "x")
                            {
                                temp_outputd = temp_outputd + "_" + l;
                            }

                            if (!CLASS_LIST.Contains(temp_outputd))
                            {
                                CLASS_LIST.Add(temp_outputd);
                            }
                        }

                        if (!CLASS_LIST.Contains(temp_outputc))
                        {
                            CLASS_LIST.Add(temp_outputc);
                        }
                    }
                    if (!CLASS_LIST.Contains(temp_outputb))
                    {
                        CLASS_LIST.Add(temp_outputb);
                    }
                }
                if (!CLASS_LIST.Contains(temp_outputa))
                {
                    CLASS_LIST.Add(temp_outputa);
                }
            }
            return CLASS_LIST;
        }
        public String Generate_G3C_Weapon_Classes(List<string> CLASS_LIST, String SHIRT_TEXTURE, String PANT_TEXTURE, String EXTENSION)
        {
            String[] CLASS_ARR = CLASS_LIST.ToArray();
            String RESPONSE = "";
            foreach (string i in CLASS_ARR)
            {
                RESPONSE = RESPONSE + "class " + i + EXTENSION + ": " + i + "{\n";
                RESPONSE = RESPONSE + "    author = \"UnderSiege Productions\";\n";
                RESPONSE = RESPONSE + "    scope = 2;\n";
                RESPONSE = RESPONSE + "    scopeArsenal = 2;\n";
                RESPONSE = RESPONSE + "    displayName = \"[USP] Crye G3C ()\";\n";
                RESPONSE = RESPONSE + "    picture = \"\\usp_gear_body\\data\\ui\\usp_icon_g3c_mc_ca.paa\";\n";
                RESPONSE = RESPONSE + "    hiddenSelectionsTextures[] = { \"" + SHIRT_TEXTURE + "\", \"" + PANT_TEXTURE + "\" };\n";

                RESPONSE = RESPONSE + "    class ItemInfo : UniformItem\n";
                RESPONSE = RESPONSE + "    {\n";
                RESPONSE = RESPONSE + "        uniformModel = \" - \";\n";
                RESPONSE = RESPONSE + "        uniformClass = \"" + i + EXTENSION + "\";\n";
                RESPONSE = RESPONSE + "        containerClass = \"Supply20\";\n";
                RESPONSE = RESPONSE + "        mass = 80;\n";

                RESPONSE = RESPONSE + "    };\n";
                RESPONSE = RESPONSE + "};\n";
            }
            return RESPONSE;
        }
        public String Generate_G3C_Vehicle_Classes(List<string> CLASS_LIST, String SHIRT_TEXTURE, String PANT_TEXTURE, String FLAG_LEFT, String FLAG_RIGHT, String EXTENSION)
        {
            String[] CLASS_ARR = CLASS_LIST.ToArray();
            String RESPONSE = "";
            foreach (string i in CLASS_ARR)
            {

                RESPONSE = RESPONSE + "class " + i + EXTENSION + ": " + i + "{\n";
                RESPONSE = RESPONSE + "    author = \"UnderSiege Productions\";\n";
                RESPONSE = RESPONSE + "    displayName = \"[USP] Crye G3C ()\";\n";
                RESPONSE = RESPONSE + "    uniformClass = \"" + i + EXTENSION + "\";\n";
                RESPONSE = RESPONSE + "    picture = \"\\usp_gear_body\\data\\ui\\usp_icon_g3c_mc_ca.paa\";\n";
                //RESPONSE = RESPONSE + "    hiddenSelectionsTextures[] = {\""+SHIRT_TEXTURE+\","","\usp_gear_body\model\tx\usp_overlord_tan_co.paa","\usp_gear_body\model\tx\usp_salomon_co.paa","","","","","\USP_Gear_Core\data\id\flag\aus_ir_mc_co.paa","\USP_Gear_Core\data\id\flag\aus_ir_mc_co.paa","","",""};";
                RESPONSE = RESPONSE + "};\n";
            }
            return RESPONSE;
        }
        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            String EXTENSION = Class_Extension.Text;
            String SHIRT_PATH = Uniform_Shirt.Text;
            String PANT_PATH = Uniform_Pant.Text;
            String UNIFORM_SET = Uniform_Set.Text;
            String L_FLAG_PATH = Flag_Left.Text;
            String R_FLAG_PATH = Flag_Right.Text;
            List<String> CLASS_LIST = new List<string>();
            String WEAPONS_OUTPUT = "";
            String VEHICLES_OUTPUT = "";
            if (UNIFORM_SET == "G3C Set")
            {
                CLASS_LIST = Generate_G3_Classes();
                WEAPONS_OUTPUT = Generate_G3C_Weapon_Classes(CLASS_LIST, SHIRT_PATH, PANT_PATH, EXTENSION);
                VEHICLES_OUTPUT = Generate_G3C_Vehicle_Classes(CLASS_LIST, SHIRT_PATH, PANT_PATH, L_FLAG_PATH, R_FLAG_PATH, EXTENSION);
            }
            Output_cfgweapons.Text = WEAPONS_OUTPUT;
            Output_cfgvehicles.Text = VEHICLES_OUTPUT;
        }
    }
}
