   M S B A M L   `   `   `  ����@  <iMTGi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:iMTGiiMTGi  5   	       iMTGi.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   %localclr-namespace:iMTGi     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     6+   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6h   X TXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   -   5          � Title$  iMTGi��5   	   $	��424��6   $	��603��6$    � WindowStartupLocation$ CenterScreen=�6G    � WindowState$ 	Maximized=�6l    � 
ResizeMode$ CanMinimize=�6�   .��5      �  � RowDefinitions 5   
   �� 5	      $
��160* �6   �� 5
      $
��161* �6    � ColumnDefinitions 5   
   �� 5      $
��300* �6   �� 5      $	��14* �6   �� 5      $	��88* �6   �� 5      $
��117* �6   .+�5   
   �� -   5         ��Name$ picture��5   2   $��3��6   $��back.png��6    62   �� 5   
   -   5         ��Name$ lv��5   $   $��2��6    �� View 5      �� 5      	 �� ColumnHeaderStringFormat$
	 Name��6   
 �� ColumnHeaderContextMenu
 5      �� 5      5      5      �� 5   
   -   5        $��textBox��5   K   $��1��6   $��1��6"    �� TextWrapping$
 Wrap=�6/   $�� ��6C    �� AcceptsReturn .  �� 
IsReadOnly . $��3��6�   �� 5   
   -   5        $��2��5      $
��Sort��6%   $��1��64   $��18��6Y   $��Bottom=�6e   R� 5   
   -   5        $��2��5      $��1��6'   $��0,0,0,18q�67   6b   5       5!      