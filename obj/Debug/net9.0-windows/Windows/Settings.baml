   M S B A M L   `   `   `  ����D  @DNDHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:DNDHelper.WindowsDNDHelper.Windows  5   	        DNDHelper.Windows.Settings   5   
   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:DNDHelper.Windows     5   	   c _MaterialDesignThemes.MahApps, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   5         MaterialDesignWindow#��[  5        � Title$  Настройки��5
   	   $	��300��6   $	��565��6(    � 
ResizeMode$ NoResize=�64   .��5      �  � ColumnDefinitions 5      �� 5      .+�5      ��  �� TabStripPlacement$
 Left=�6   $
��Left=�6(   $	��550��6C   .@�5      �� $��Профили��6   $	��100��6   .��5      �  � RowDefinitions 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6    5      �� 5      $��* �6   �� 5      $
��Auto �6   .+�5      �� -   5         ��Name$ NameProfile��5      $��0��6$   $��0��61   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Название��6A   �� 5      -   5         ��Name$ 
AddProfile��5      $��0��6"   $��1��6/   $��Добавить��6?   6R   �� 5      -   5        $ 
UrlProfile��5      $��1��6#   $��0��60   $��2��6@    �� TextWrapping$
 Wrap=�6T   $ Ссылка��6h   �� 5      -   5        	 ��Name$	 ListProfiles��5      $��2��6%   $��0��62   $��2��6B   .@�5       �� $��2��6   .��5!   	   �� .+�5"   
   �� $��Гюнтер��6   $
��Bold"�6"   �� 5#   
   $]��Whttps://raw.githubusercontent.com/GunterSuperPenguin/dnd-configs-gunter/refs/heads/main��6   $
��Wrap=�6s   5$   
   5%   	   �� 5&      .��5'   	   �� .+�5(   
   �� $��Туша��6   $
��Bold"�6    �� 5)   
   $5��/https://github.com/Chucapabra/dnd/tree/Settings��6   $
��Wrap=�6K   5*   
   5+   	   5,      5-      5.      �� 5/      $��Программа��6   .��50      �� .+�51      �� $D��>Движение текста при переполнении.��6   $��5,5,0,5q�6<   �� 52      
 �� Orientation$
 
Horizontal=�6   $��5q�6+   .+�53      �� $��
Тема: ��6   $��0,5,5,5q�6    �� 54      -   5         ��Name$ ComboBoxTheme��54   )   $	��100��6   $��30��6   .@�55   	   �� $��Светлая��6   �� 56   	   $��Тёмная��6   57   	   58      59      5:      5;      5<      5=      