   M S B A M L   `   `   `  ����@  <Torun, Version=1.0.1.1, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Torun.Windows.WeeklyPlanTorun.Windows.WeeklyPlan  5   	   )    #Torun.Windows.WeeklyPlan.RemovePlan   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Torun.Windows.WeeklyPlan     5   	   -   5          � Icon$  /images/torun.ico��5   >    � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterOwner=�60    � WindowStyle$
 None=�6T   ���#B0151E$��0,0,0,5q�5       � Title$ MarkCompleted��5
   	   $	��150��6   $	��420��6,   .��5   
   � 
������� � RowDefinitions 5      �� 5      $��40 �6    6.   �� 5      $	��110 �6    6/   .+�5      �� $��0��6   ._�5      �  � ColumnDefinitions 5      �� 5      �� 5      $��40 �6+   .+�5      �� $��0��6   ._�5      �� -   5         ��Name$ remove_title��5   q   $	��XXX��6    $��17(�6/   $��5q�6=   
����������#B0151E6q   5      �� 5      $��1��6   ._�5      �� -   5         ��Name$ btnClose��5   W   $��X��6!      buttonMiniClose#���   6-   6i   5      5      5      �� 5      $��1��6   ._�5      �  5       �� 5!      �� 5"      $��95 �6'   .+�5$      �� $��0��6   $
��Left=�6)   $��Center=�6D   ._�5%      �� .+�5&      	� -   5        	 	�Name$	 remove_aDay��5&   P     completedRadios#���  6&   $	��XXX��6e   
 d� 	IsChecked$

 TrueY�6s    	� 	GroupName$ 
removePlan��6�   	� 5'      -   5        $	 remove_allDays��5'   P   #���  6&   $	��XXX��6h   $
 FalseY�6v   $ 
removePlan��6�   5(      5)      �� 5*      $��1��6   $��Right=�6)   ._�5+      �� -   5        $ 
removeSave��5+   �     buttonRedBorder2#���  6   $��90��6H   $��33��6S   $��0,10,5,0q�6_   
�������$	��XXX��6�   6�   5,      5-      5.      5/      51      