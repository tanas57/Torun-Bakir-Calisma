   M S B A M L   `   `   `  ����	  Torun-    'Torun.Windows.CheckList.EditRoutineWork   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � Icon$  /images/torun.ico��5   /    � Title$ 
RequestAdd��5   	   ���#B0151E$��0,0,0,5q�62   $	��200��6L   $	��350��6Y    � 
ResizeMode$ NoResize=�6e    � WindowStyle$
 None=�6{   ��6�   Z� .��5      � 
������� � RowDefinitions 5	   
   �� 5
      $��40 �6   6*   �� 5      6   .+�5   
   �� $��0��6   ._�5      �  � ColumnDefinitions 5      �� 5      $	��270 �6'   �� 5      .+�5      �� $��0��6   ._�5      �� -   5         ��Name$ routinWorkTitle��5   m   $	��XXX��6   $��17(�6+   $��5q�69   
����������#B0151E6m   5      �� 5      $��1��6   ._�5      �� -   5         ��Name$ req_btnClose��5   S   $��X��6      buttonMiniClose#���   6)   6i   5      5      5      �� 5   
   $��1��6   ._�5      �� .+�5      �� -   5        $ workDescription��5      $	��XXX��61   ]� 5      $��0,10,0,0q�6   .+�5      �� $	��267��6    $��5,0,0,5q�6,   �� 6>   .F�5       �� -   5         ��Name$ dbDescription��5    3   $��0��6&   $	��257��6J   $��80��6V   	 �� TextWrapping$	 WrapWithOverflow=�6b   5!      6/   �� 5"      -   5        $
 save��5"   p   $��Center=�6   $��Center=�6:   $��6��6U   
 � Cursor$

 Hand��6b   .��5#      ]� .+�5$      �� -   5        $ saveLbl��5$   C   $��0,0,0,0q�6$   $	��XXX��65     label_weeklyPlan#���  6T   $��33��6   $��80��6�   5%      5&      5'      �� 5(      -   5        $ result��5(      $��0,10,0,0q�6(   5)      5+      5,      5-      