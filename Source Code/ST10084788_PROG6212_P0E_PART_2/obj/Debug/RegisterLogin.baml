   M S B A M L   `   `   `  ����Y  UST10084788_PROG6212_P0E_PART_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ST10084788_PROG6212_P0E_PART_2ST10084788_PROG6212_P0E_PART_2  5   	   2    ,ST10084788_PROG6212_P0E_PART_2.RegisterLogin   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   >local,clr-namespace:ST10084788_PROG6212_P0E_PART_2     5   	     � Title$  RegisterLogin��5   	   $	��450��6   $	��380��6,    � AllowsTransparency . 
������  � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�6    � WindowStartupLocation$ CenterScreen=�62   .��5      � .+�5   
   �  � RowDefinitions 5      �� 5      �� 5      .+�5      �� $��2��6   ��5      �� 5      .��5      �  � Color$ Black��6'    � Offset$	 0.2Q�65   � 5      $ 	SteelBlue��6'   $	 1.3Q�69   5      5      �� 5"      $��Center=�6   .+�5#      �� $��Icons/logo.png��6   $	��150��60   $��50��6<   �� 5$      -   5         ��Name$ 
TitleBlock��5$      $��TIME MANAGEMENT APP��60   $��Light"�5%      $��	Helvetica)�5&      $��22(�5'      
�������$��Center=�5)      5+      �� 5/      $��1��6   .+�51      �� 	 �� Orientation$	 
Horizontal=�6   .+�53      �� $��	USERNAME:��6   $��74��60   $��	Helvetica)�6<   $��Light"�54      $��12(�55      
�������
������ $��30,0,0,0q�58      �� 59      -   5        
 ��Name$
 UsernameBox��59      $��	Helvetica)�63   $��Light"�5:      $��20(�5;      
�������
������ $��0q�5>      $	��185��5?       J� Opacity$	 0.5Q�5@      $��25��5A      $��1,0,0,0q�5B      5D      �� 5H      $	��200��6   $��2��5I      $	 0.5Q�5J      
�������$��30,0,0,0q�5L      �� 5P      $	 
Horizontal=�6   $��0,20,0,0q�67   .+�5R      �� $��	PASSWORD:��6   $��77��60   $��	Helvetica)�6<   $��Light"�5S      $��12(�5T      
�������
������ $��26��5V   3   $��30,0,0,0q�5W      G� 5X      -   5         G�Name$ PasswordBox��5X   "   $��	Helvetica)�67   $��Light"�5Y      $��20(�5Z      
�������
������ $��0q�5]      $	��179��5^      $
��Left=�5_      $	 0.5Q�5`      $��26��5a      $��1,0,0,0q�5b      5e      �� 5i      $	��196��6   $��2��5j      $	 0.5Q�5k      
�������$��30,0,0,0q�5m      �� 5q      $	 
Horizontal=�6   $��0,50,0,0q�66   .+�5r      �� -   5         ��Name$ LoginBtn��5r      $	��100��6/   $��40��6;   $��LOGIN��6G   $��	60,0,60,0q�6W   6j   �� 5s      -   5        $ RegisterBtn��5s      $	��100��62   $��40��6>   $��REGISTER��6J   6]   5t      5w      5z      5}      5      