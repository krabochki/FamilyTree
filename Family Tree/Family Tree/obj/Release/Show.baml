   M S B A M L   `   `   `  ���� F  BFamily Tree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Family_TreeFamily_Tree      Family_Tree.Show   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Family_Tree     -     � 
ResizeMode$  CanMinimize=�
 � Top$ 0�� � Left$ 0�� � Title$ Family Tree��$	��150��$	��650�� � 	Resources  �� 
TargetType �� Property �� Value   Regular��%�   '��       7   �� " ��.���  � ��$ 14(���  � ��8   ��  �@��TextAlignment$ Center=���  � ��
 �������  � ��$ Center=���  � ��$
 Wrap=���  � ��$ Center=�.��� ����  �� 
StartPoint$	 0,0&�	 �� EndPoint$		 1,1&�.��� 
 � Color$
 	#FF43CCA2�� � Offset$ 0Q�� $
 	#FF195A9B��$ 1Q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $	��250 ��� .+��� -    ��Name$ 
imageblock��$��1��$��0,2,0,0q�$��Right=�$��Center=�.+��� $��	0 20 0 20q�$
��Auto��._�� .+��� -   $
��Mask��
�������
������  �� CornerRadius$	 100����  J� OpacityMask !�  !� Visual ��  �� ElementName$
 Mask��.+��� -    ��Name$ personPhoto_image��$	��200��$��UniformToFill=�$	��200��$��Assets/Images/man.png���� -   $ 
stackpanel��$��0��$��2��$��Bottom=�$��Center=�$	��550��.+��� -    ��Name$ fullname_textblock��$��	0 35 0 10q�$��25(�  	ExtraBold#��[ $
��Wrap=�$8��2Кузьменок Полина Сергеевна�� �� TextAlignment$ Center=�$��Center=�$��Bottom=��� -    ��Name$ 
geo_button��$��0��$��1��$��30��$��30��$��Right=�$��Bottom=�$��	0 0 30 10q�  IconButtonStyle#��[ 
 � Tag$ Assets/Images/geo.png�� � ToolTip _�   ToolTipStyle#��[ $1��+Найти это место в Google Maps���� -   $ 	textblock��$��Center=�$
��Left=�$��
40 20 40 0q�$
��Wrap=�$��1��$��1��$
 Left=��� -	   $ comment_block��$
��Auto��$��40 10 40 20q�$��2��$��0��$��2��$	��Top=�$��Center=�  Bold#��[ 