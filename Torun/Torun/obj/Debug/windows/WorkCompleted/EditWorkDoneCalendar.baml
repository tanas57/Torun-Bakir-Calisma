   M S B A M L   `   `   `  ����@  <Torun, Version=1.0.1.1, Culture=neutral, PublicKeyToken=nullI)clr-namespace:Torun.Windows.WorkCompletedTorun.Windows.WorkCompleted  5   	   6    0Torun.Windows.WorkCompleted.EditWorkDoneCalendar   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:Torun.Windows.WorkCompleted     5   	   -   5          � Icon$  /images/torun.ico��5   A    � 
ResizeMode$ NoResize=�5       � WindowStartupLocation$ CenterOwner=�6)    � WindowStyle$
 None=�6M   ���#B0151E$��0,0,0,5q�5       � Title$ MarkCompleted��5
   	   $	��277��6   $	��222��6,   .��5      � 
������� � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $	��240 �6   .+�5   
   �� $��0��6   ._�5      �  � ColumnDefinitions 5      �� 5      �� 5      $��40 �6'   .+�5      �� $��0��6   ._�5      �� -   5         ��Name$ calendarTitle��5   m   $	��XXX��6   $��17(�6+   $��5q�69   
����������#B0151E6m   5      �� 5      $��1��6   ._�5      �� -   5         ��Name$ btnClose��5   S   $��X��6      buttonMiniClose#���   6)   6e   5      5      5      �� 5   
   $��1��6   $��Center=�6   $��Center=�6;   ._�5      �� .+�5       �� -   5        $ dateChoosetxt��5       $	��XXX��6/   $��Center=�6=   &   System.Windows.Controls.Calendar  5!      -   5        	  Name$	 calendar��5!   k   
   IsTodayHighlighted
 .     SelectionMode$ 
SingleDate=�6}   �� 5"      -   5        $
 save��5"   >   {�.  $	��XXX��6L     planEditButtons#���  6Z   5#      5$      5%      5&      