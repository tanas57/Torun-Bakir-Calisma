   M S B A M L   `   `   `  ����@  <Torun, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null=#clr-namespace:Torun.Windows.RequestTorun.Windows.Request  5   	   )    #Torun.Windows.Request.RequestDetail   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Torun.Windows.Request     5   	   -   5          �Name$  req_DetailTitle��5	   �    � Icon$ /images/torun.ico��5   +    � Title$ RequestDetail��5	   	   ���#B0151E$��0,0,0,5q�6Q    � WindowStyle$
 None=�6k   $	��370��6�   $	��320��6�    � WindowStartupLocation$ CenterOwner=�6�    � 
ResizeMode$ NoResize=�6�   ��6~   Z� .��5
      � 
������� � RowDefinitions 5   
   �� 5      $��40 �6   6*   �� 5      6   .+�5   
   �� $��0��6   ._�5      �  � ColumnDefinitions 5      �� 5      $	��170 �6'   �� 5      .+�5      �� $��0��6   ._�5      �� -   5         ��Name$ req_RequestAdd��5   m   $	��XXX��6   $��17(�6+   $��5q�69   
����������#B0151E6m   5      �� 5      $��1��6   ._�5      �� -   5        	 ��Name$	 req_btnClose��5   S   $��X��6      buttonMiniClose#���   6)   6i   5      5      5      �� 5   
   $��1��6   ._�5      �� $��Center=�6   $��Center=�66   .+�5      ]� $��0,10,0,0q�6   .+�5       �� $��Right=�6    �� 5!      -   5        $ lbl_reqNumber��5!   *   $	��XXX��6   6*    �� 5"      -   5        
 ��Name$
 
req_Number��5"      $	��190��62   $��30��6>   5#      5$      ]� 5%      $��0,15,0,0q�6   .+�5&      �� $��Right=�6    �� 5'      -   5        $ lbl_reqPriority��5'   *   $	��XXX��6   6*    �� 5(      -   5         ��Name$ req_Priority��5(   1    �� SelectedIndex$ 0��6   $	��191��6G   $��30��6S   5)      5*      ]� 5+      $��0,15,0,0q�6   .+�5,      �� $��Right=�6    �� 5-      -   5        $ lbl_reqDescription��5-   *   $	��XXX��6   6*    �� 5.      -	   5        $
 req_Description��5.   ?    �� TextWrapping$
 Wrap=�6   $	��140��62   $	��222��6X   5/      50      ]� 51      $��0,15,0,0q�6   .+�52      �� -
   5        $	 req_Save��52      $	��150��6F   $��Right=�6R   .��53      ]� .+�54      �� ��6$   Z� �� 55      -   5        $ lbl_req_Button��55   2   $	��XXX��6$   62   56      57      58      �� 59      -   5        $ 
req_Result��59      $	��XXX��6,   
���  ��$��0,10,0,0q�6K   
�������$y�Hidden=�6p   5:      5;      5=      5>      