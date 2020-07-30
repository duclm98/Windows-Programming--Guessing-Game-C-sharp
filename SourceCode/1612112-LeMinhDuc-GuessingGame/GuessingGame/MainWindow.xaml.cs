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
using System.IO;

namespace GuessingGame
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

        string[] nameAnimals = { "Tiger", "Lion","Zebra","Leopard", "Hippo",
                                 "Baboon","Eagle","Rhinoceros","Hyena","Camel",
                                 "Hedgehog","Wolf","Parrots","Crocodile","Chimpanzee",
                                 "Giraffe","Vulture","Ostrich","Gazelle","Seal",
                                 "Cobra","Owl","Pangolin","Peacock","Mantis","Jellyfish" };//Có 26 animal
        //Các chỉ số của mỗi nameAnimal trùng với tên ảnh trong folder ImageAnimals

        char[] indexArr = { 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n',
                            'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n', 'n',
                            'n', 'n', 'n', 'n', 'n', 'n'};
        Random rd = new Random();
        int soLanChonConLai = 3;

        private int RandomAnimal(Random rd,char[]indexArr)
        {
            Int32 index = rd.Next(0, 2599);
            while(indexArr[index/100]=='y')
            {
                index = rd.Next(0, 2599);
            }
            indexArr[index/100] = 'y';
            return index/100;
        }


        private void ShowMainWindows()
        {
            int index = RandomAnimal(rd, indexArr);//Là chỉ số của 1 animal trong mảng animals
            int index1 = RandomAnimal(rd, indexArr);

            textBlockNameAnimal.Text = nameAnimals[index];

            string strIndex, strIndex1;
            if (index < 10)
                strIndex = "0" + index.ToString();
            else
                strIndex = index.ToString();
            if (index1 < 10)
                strIndex1 = "0" + index1.ToString();
            else
                strIndex1 = index1.ToString();

            String[] pathArr = new String[2];
            pathArr[0] = Directory.GetCurrentDirectory().ToString() + "\\ImageAnimals\\" + strIndex + ".jpg";
            pathArr[1] = Directory.GetCurrentDirectory().ToString() + "\\ImageAnimals\\" + strIndex1 + ".jpg";

            //Phát sinh ra 2 số 0, 1 ngẫu nhiên vào biến a và b
            int a = rd.Next(0, 199) / 100;
            int b = 1 - a;
            string path1 = pathArr[a];//Là một trong hai đương dẫn file ở mảng pathArr
            string path2 = pathArr[b];//Là một trong hai đương dẫn file ở mảng pathArr
            image.Source = new BitmapImage(new Uri(path1));
            image1.Source = new BitmapImage(new Uri(path2));
        }

        private void ClickButton(string pathImage)//pathImage là đường dẫn từ image VD: C\ImageAnimals\1.jpg
        {
            string nameAni = textBlockNameAnimal.Text;
            int length = pathImage.Count();
            string nameImage = pathImage.Substring(length - 6);//Lấy tên của image VD: 01.jpg
            nameImage = nameImage.Substring(0, 2);//Lấy tên sau khi đã bỏ đi phần đuôi VD: 1

            int i;//i là chỉ số của animal trong mảng nameAnimals

            for (i = 0; i < nameAnimals.Count(); i++)
            {
                if (nameAni == nameAnimals[i])
                    break;
            }

            Int16 number = Int16.Parse(textBlockQuestion.Text); //Là câu hỏi đang thực hiện

            if (i != Int16.Parse(nameImage))//Nếu chọn sai
            {
                soLanChonConLai--;
                textBlockValueScore.Text = Int16.Parse(textBlockValueScore.Text).ToString();//Hiền thị số điểm hiện tại
                MessageBox.Show("CHỌN SAI RỒI!!!");
            }
            else//Nếu chọ đúng
            {
                number++;
                textBlockValueScore.Text = (Int16.Parse(textBlockValueScore.Text) + 1).ToString();//Hiền thị số điểm hiện tại
            }

            textBlock5.Text = soLanChonConLai.ToString();//Hiển thị số lần còn có thể chọn lại

            if (soLanChonConLai != 0 && number <= 10)
            {
                ShowMainWindows();
                textBlockQuestion.Text = number.ToString();
            }
            else if (soLanChonConLai == 0)
            {
                MessageBox.Show("GAME OVER!!!");
                string score = "SCORE: " + textBlockValueScore.Text;
                MessageBox.Show(score);
                this.Close();
            }
            else if (number > 10)
            {
                MessageBox.Show("DONE!!!");
                string score = "SCORE: " + textBlockValueScore.Text;
                MessageBox.Show(score);
                this.Close();
            }
        }


        private void MainWindowsLoaded(object sender, RoutedEventArgs e)
        {
            ShowMainWindows();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string pathImage = image.Source.ToString();//Lấy đường dẫn từ image VD: C\ImageAnimals\1.jpg
            ClickButton(pathImage);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string pathImage1 = image1.Source.ToString();//Lấy đường dẫn từ image VD: C\ImageAnimals\1.jpg
            ClickButton(pathImage1);
        }
    }
    
}
