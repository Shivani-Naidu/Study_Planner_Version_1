   M S B A M L   `   `   `  ����Y  UST10084788_PROG6212_P0E_PART_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ST10084788_PROG6212_P0E_PART_2ST10084788_PROG6212_P0E_PART_2  5      2    ,ST10084788_PROG6212_P0E_PART_2.SelfStudyPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:ST10084788_PROG6212_P0E_PART_2     5      $
��1020��5      $	��750��6     N� Title$  SelfStudyPage��5	      .��5      � .+�5   
   �� ��5      �� 5      .��5      �  � Color$ Black��6#    � Offset$	 0.6Q�61   � 5      $ 	SteelBlue��6#   $	 1.3Q�65   5      5      �� 5   
   -   5         ��Name$ ViewModules��5      $��Click To View Modules��6&   $��Center=�6F   $��0,70,0,0q�6c   $	��Top=�6u   $��43��6�   $	��158��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$	 DG2��5   '   $y�Hidden=�6      RowBackground
 ���� 
������ 
�������$��303,131,291,435q�6~      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 150 6�    � 	Resources 5      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 5      "  6   .�5      -   5        5      5      	   ColumnHeaderStyle	 5      �� 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5      �� 
 �� Property
 � �� �� Value
 ���� 63   5!         RowHeaderStyle 5$      �� 5%      :  4System.Windows.Controls.Primitives.DataGridRowHeader"  6   .�5&      �� 
 � ��
 ���� 63   5'      5)      �� 5*   
   -   5         ��Name$ 
MCodeBlock��5*   (   $y�Hidden=�6   
�������$��16(�6P   $
��Left=�6^   $��205,415,0,0q�6y   $
��Wrap=�6�   $��Module Code:��6�   $	��Top=�6�   �� 5+   
   -   5        $ 
SNameBlock��5+   (   $y�Hidden=�6   
�������$��16(�6P   $
��Left=�6_   $��202,470,0,0q�6z   $
��Wrap=�6�   $��Semester Name:��6�   $	��Top=�6�    J� RenderTransformOrigin$ 0.467,-0.423&�6�   (  "System.Windows.Controls.DatePicker  5,   
   -   5          Name$ DatePicker3��5,   )   $y�Hidden=�6   $
��Left=�6>   $��337,521,0,0q�6Y   $	��Top=�6n   $ 0.545,0.475&�6�   $	��153��6�   �� 5-   
   -   5         ��Name$ HoursStudyBox��5-   &   $y�Hidden=�6   $
��Left=�6=   $��337,581,0,0q�6X    �� TextWrapping$
 Wrap=�6m   $	��Top=�6�   $	��153��6�   �� 5.   
   -   5        $ StudyDateBlock��5.   (   $y�Hidden=�6   
�������$��16(�6T   $
��Left=�6b   $��205,522,0,0q�6}   $
��Wrap=�6�   $��Date Studied:��6�   $	��Top=�6�   �� 5/   
   -	   5        $ 
HoursBlock��5/   (   $y�Hidden=�6   
�������$��16(�6P   $
��Left=�6^   $��202,578,0,0q�6y   $
��Wrap=�6�   $��Hours Studied:��6�   $	��Top=�6�   �� 50   
   -
   5        $ StudySessionBtn��50   %   $y�Hidden=�6   $��Add Study Session��6>   $
��Left=�6Z   $��337,644,0,0q�6u   $	��Top=�6�   $��31��6�   $	��120��6�   $ 0.5,0.5&�6�   6�     51   
   -   5        $	 DG4��51   q   $y�Hidden=�6   
 ���� 
������ 
�������$��527,436,19,44q�6   $	 150 6�   	 52      �� 53      "  6   .�54      �� 
 � ��
 ���� 63   56       59      �� 5:      "  6   .�5;      �� 
 � ��
 ���� 63   5<      5>      �� 5?   
   -   5        $ ModuleCodeBlock��5?   J   $y�Hidden=�6   
�������$��16(�6<   $
��Left=�6c   $��337,415,0,0q�6~   $
��Wrap=�6�   $��	TextBlock��6�   $	��Top=�6�   $	��120��6�   �� 5@   
   -   5        $ SemesterNameBlock��5@   J   $y�Hidden=�6   
�������$��16(�6<   $
��Left=�6e   $��337,470,0,0q�6�   $
��Wrap=�6�   $��	TextBlock��6�   $	��Top=�6�   $	��100��6�   �� 5A   
   -   5        $ SummaryBlock��5A   )   $y�Hidden=�6   
�������$
��Left=�6S   $��747,156,0,0q�6n   $
��Wrap=�6�   $	��Top=�6�   $	��124��6�   $	��254��6�   �� 5B   
   -   5        $ ViewSessionsBtn��5B   %   $y�Hidden=�6   $$��Click View Self-Study Sessions��6>   $
��Left=�6g   $��798,371,0,0q�6�   $	��Top=�6�   $��43��6�   $	��203��6�   6�   �� 5C   
   -   5         ��Name$ TitleLbl��5C   %   $y�Hidden=�6   $��	Helvetica)�67   
�������$��10(�6b   $_��YDouble Click On A Module To Add A Self-Study Session And View Statistics For That Module.��6p   $
��Left=�6�   $��303,315,0,0q�6�   $	��Top=�6  $��29��6  $	��438��6(  �� 5D   
   -   5        $ studyLbl��5D   (   $y�Hidden=�6   
�������$��16(�6N   $
��Left=�6\   $��205,375,0,0q�6w   $
��Wrap=�6�   $��Add A Self-Study Session��6�   $	��Top=�6�   �� 5E   
   -   5        $ Notify2��5E   6   $y�Hidden=�6   $��10(�6(   $
��Left=�6G   $��338,599,0,0q�6b   $
��Wrap=�6w   $��	TextBlock��6�   $	��Top=�6�   $	��152��6�   $��30��6�   �� 5F   
   -   5        $ Notify1��5F   6   $y�Hidden=�6   $��10(�6(   $
��Left=�6G   $��338,545,0,0q�6b   $
��Wrap=�6w   $	��Top=�6�   $	��157��6�   $��26��6�   �� 5G   
   $��16(�6   
�������$
��Left=�66   $��
461,10,0,0q�6Q   $
��Wrap=�6e   $��Self-Study Page��6y   $	��Top=�6�   5K      5L      