   M S B A M L   `   `   `  ����@  <Torun, Version=1.0.1.1, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Torun.Windows.WeeklyPlanTorun.Windows.WeeklyPlan  5   	   ,    &Torun.Windows.WeeklyPlan.MarkCompleted   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Torun.Windows.WeeklyPlan     5   	   -   5          � Icon$  /images/torun.ico��5   >    � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterOwner=�60    � WindowStyle$
 None=�6T   ���#B0151E$��0,0,0,5q�5       � Title$ MarkCompleted��5
   	   $	��320��6   $	��400��6,   .��5      � 
������� � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $	��290 �6   .+�5   
   �� $��0��6   ._�5      �  � ColumnDefinitions 5      �� 5      �� 5      $��40 �6'   .+�5      �� $��0��6   ._�5      �� -   5         ��Name$ completed_title��5   m   $	��XXX��6   $��17(�6+   $��5q�69   
����������#B0151E6m   5      �� 5      $��1��6   ._�5      �� -   5         ��Name$ btnClose��5   S   $��X��6      buttonMiniClose#���   6)   6e   5      5      5      �� 5   
   $��1��6   $��Center=�6   $��Center=�6;   ._�5      �� .+�5       ]� .+�5!      	� -   5        	 	�Name$	 completed_aDay��5!   L     completedRadios#���  6"   $	��XXX��6d   
 d� 	IsChecked$

 TrueY�6r    	� 	GroupName$ isCompleted��6�   	� 5"      -   5        $	 completed_allDays��5"   L   #���  6"   $	��XXX��6g   $
 FalseY�6u   $ isCompleted��6�   5#      ]� 5$      $��0,15,0,0q�6   $
��Left=�6.   .+�5%      �� -   5         ��Name$ todayCompleted��5%   1   $��0,5,11,0q�6   $

 TrueY�6I   $��Bugün mü tamamlandı ?��6Z   6�   (  "System.Windows.Controls.DatePicker  5&      -   5          Name$ 
chooseDate��5&   ?   {�.  $	��100��63      IsTodayHighlighted .  6S   5'      �� 5(      $��0,10,0,0q�6   .+�5)      �� -   5        $ completedNote��5)   *   $	��XXX��6   6*   �� 5*      -	   5         ��Name$ DbcompletedNote��5*   X   $	��100��6   $	��390��6+   $��3q�67    �� TextWrapping$
 Wrap=�6C   6X   5+      �� 5,      -
   5        $ completed_save��5,   �     buttonRedBorder2#���  6   $	��100��6D   $��28��6P   $��0,10,0,0q�6\   
�������$	��XXX��6�   6�   �� 5-      -   5        $ result��5-   E   $y�Hidden=�6   $��3q�6,   $	��XXX��67   
�������
���  ��$��16(�6y   5.      5/      50      51      