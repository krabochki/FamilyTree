   M S B A M L   `   `   `  ����F  BFamily Tree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Family_TreeFamily_Tree  5   	       Family_Tree.Show   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Family_Tree     5   	   -   5          � 
ResizeMode$  CanMinimize=�5   	   
 � Top$ 0��5	   	    � Left$ 0��5
   	    � Title$ Family Tree��5   	   $	��150��6   $	��650��6*    � 	Resources 5       �� 
TargetType �� Property �� Value   Regular��%H  '��       7   �� 5   
   " ��6   .�5      ��  � ��$ 14(�6)   �� 5       � ��8   6+   �� 5       �@��TextAlignment$ Center=�6.   �� 5       � ��
 �����6+   �� 5       � ��$ Center=�64   �� 5       � ��$
 Wrap=�6-   �� 5       � ��$ Center=�62   5      5      .��5      � ��5    
   �� 5!       �� 
StartPoint$	 0,0&�6"   	 �� EndPoint$		 1,1&�63   .��5"      � 
 � Color$
 	#FF43CCA2��6    � Offset$ 0Q�61   � 5#      $
 	#FF195A9B��6   $ 1Q�61   5$       � RowDefinitions 5'   
   �� 5(      $
��Auto �6   6,   �� 5)      $
��Auto �6   6,   �� 5*      $
��Auto �6   6,    � ColumnDefinitions 5,   
   �� 5-      $	��250 �6   �� 5.      .+�53   
   �� -   5         ��Name$ 
imageblock��53   k   $��1��6   $��0,2,0,0q�6#   $��Right=�64   $��Center=�6P   .+�56      �� $��	0 20 0 20q�6   $
��Auto��57      ._�58      � .+�59      �� -   5        $
��Mask��59      
�������
������  �� CornerRadius$	 100��5<      5=      �� 5>       J� OpacityMask 5?      !� 5@       !� Visual 6*   ��  �� ElementName$
 Mask��.+�5B      �� -   5         ��Name$ personPhoto_image��5B   !   $	��200��6<   $��UniformToFill=�6H   $	��200��6a   $��Assets/Images/man.png��5C       5D      5E      5F      5G      �� 5I   
   -   5        $ 
stackpanel��5I   9   $��0��6   $��2��6%   $��Bottom=�6M   $��Center=�6h   $	��550��6�   .+�5J      �� -   5         ��Name$ fullname_textblock��5J   +   $��	0 35 0 10q�6   $��25(�6G     	ExtraBold#��[ 6U   $
��Wrap=�6~   $8��2Кузьменок Полина Сергеевна��6�    �� TextAlignment$ Center=�6�   $��Center=�6�   $��Bottom=�6�   5K      �� 5O   
   -   5         ��Name$ 
geo_button��5O      $��0��6%   $��1��62   $��30��6B   $��30��6M   $��Right=�6Y   $��Bottom=�6u   $��	0 0 30 10q�6�     IconButtonStyle#��[ 6�   
 � Tag$ Assets/Images/geo.png��6�    � ToolTip 5P      _� 5Q        ToolTipStyle#��[ 6   $1��+Найти это место в Google Maps��6@   6j   5S      �� 5V   
   -   5        $ 	textblock��5V   �   $��Center=�68   $
��Left=�6S   $��
40 20 40 0q�6o   $
��Wrap=�6�   $��1��6�   $��1��6�   $
 Left=�6�   6�   �� 5X   
   -	   5        $ comment_block��5X   7   $
��Auto��6   $��40 10 40 20q�6"   $��2��6N   $��0��6[   $��2��6k   $	��Top=�6   $��Center=�6�     Bold#��[ 6�   6�   5Y      5[      