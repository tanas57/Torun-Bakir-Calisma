   M S B A M L   `   `   `  ���� 	  Torun    Torun.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � Title$  Plan Takip - Torun��$	��650��$
��1000��$	��600��$	��800�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � Icon$  images/icon/topbarlogo.png��.���  � RowDefinitions �� $	��170 ���  � ColumnDefinitions �� $��0 ��� $��44.841 ��� $��175.159 ��� .+��� 
����  �$��0��$��0��$��3��._��  �� $��55 ��� .+��� -    ��Name$ brd_topLogo��$��0��._�]� $��Center=�$��Center=�.+��� $!��/images/icon/topbarlogo.png��$��32��$��32���� $��Torun - Plan Tracer��
�������$
��Bold"�$��Center=�$��Center=��� $��1��._�]� $��Center=�$��Center=�.+��� $��	0,-33,0,0q�����  �� 
TargetType" ��.��� 	 �� Property	 � ��
 �� Value$$
 /images/icon/biglogo_white.png���� 	 � ��$	
 140���� 	 � ��$	
 140�� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ��� 	 � ��$#
 /images/icon/biglogo_blue.png���� 	 � ��$	
 150���� 
����  �$��3��$��0��._��  J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect   �� $��35 ��� .+��� -   $ brd_top��$��0��
�������._�]� $��Right=�$��75��.+��� -    ��Name$ btnFormDown��$��__��   miniButtons#���   �� -   $ btnClose��$��X��#���   �� $��1��
����  �._��  �� �� .+��� $��0��._��� $��	30,20,0,0q�.+�]� .+��� $(��"Toplam kayıtlı talep sayısı : ��
�������$��19(��� $	��125��
����� �$��19(�]� .+��� $��Açık talep : ��
�������$��14(��� $��Welcome!��
������$��14(��� $��Kapatılmış talep : ��
�������$��14(��� $��Welcome!��
��� ���$��14(��� $��1��._��� $��	30,20,0,0q�.+�]� .+��� $��/images/icon/employee.png��$	��100���� $��M.Tayyip Muslu ,��
�������$��19(�$��40���� $��hoşgeldiniz��
�������$��19(�$��40��]� $��110, -35, 0,0q�.+��� $��50����Z� .��]� $��0,0,0,0q�.+��� $��/images/icon/exit.png��$��24��$��24���� $��Çıkış yap��
�������$��17(��� 
����  �$��0��$��1��$��3��._��� .+��� -   $ btn_toDo��$��50��  menuButtons#���  .��]� $	��200��$��15,0,0,0q�.+��� $ ��/images/icon/menu_todo.png��$��32���� $��Yapılacaklar��
�������$��16(��� $��50��#���  .��]� $	��200��$��15,0,0,0q�.+��� $$��/images/icon/menu_planning.png��$��32���� $��Haftalık Plan��
�������$��16(��� $��50��#���  .��]� $	��200��$��15,0,0,0q�.+��� $%��/images/icon/menu_completed.png��$��32���� $��Yapılmış İşler��
�������$��16(��� $��50��#���  .��]� $	��200��$��15,0,0,0q�.+��� $��/images/icon/calendar.png��$��32���� $��Rapor��
�������$��16(��� $��50��#���  .��]� $	��200��$��15,0,0,0q�.+��� $��/images/icon/about.png��$��32���� $��About��
�������$��16(��� .+���  �� Left$ 70�� �� CornerRadius$ 10,10,10,10��._��� $��0.0.0.1��
�������$��71��$��Center=�$��Center=� � Cursor$
 Hand�����	#7F054273���� " ��.��� 	 � ��
 �	#99384757 P�  �@J�IsMouseOver . . ��� 	 � ��
 �	#CC053D76�� $��3��$��1��._�� -    �Name$ Grd_Content��