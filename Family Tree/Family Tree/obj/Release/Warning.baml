   M S B A M L   `   `   `  ���� F  BFamily Tree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Family_TreeFamily_Tree      Family_Tree.Warning   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Family_Tree       � WindowStartupLocation$  CenterScreen=� � 
ResizeMode$ NoResize=� � Title$ Warning��$	��225��$	��360������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF43CCA2��� $ 	#FF195A9B�� � Offset$ 1Q�.���  � RowDefinitions �� $��* �.+��� $��Center=�$��0��.+��� -    ��Name$ warning_textblock��$
��Wrap=�$����Новая персона должна иметь по крайней мере имя для распознавания! Пожалуйста, добавьте его.��$	��276��
�������$��16(�	 �� TextAlignment$	 Center=� 
  Medium#��[  � -   
 �Name$

 grid��$	��276�� � ColumnDefinitions �� $��* ��� $��* �.+��� -    ��Name$ OKbutton��$��0 20 0 0q�$��2��$��0��$��Хорошо��$��17(����	#30FFFFFF
�������$��2q� � Cursor$
 Hand��
�������$��Center=�$��Center=�$	��120��$��34��  Bold#��[ ����  �� 
TargetType" ��.���  �� Property � �� �� Value �	#30FFFFFF �� Triggers P�  P� Property �@J�IsMouseOver P� Value .  N� EnterActions �� .���� .����  �� TargetProperty$1 +(Button.Background).(SolidColorBrush.Color)�	 �� To$ 	#45FFFFFFX� g� Duration$ 0:0:0.2D� N� ExitActions �� .���� .���� $1 +(Button.Background).(SolidColorBrush.Color)�$ 	#30FFFFFFX�$ 0:0:0.2D�����  �� 
TargetType" ��.R��� #��z��#��z��#��z�� �� CornerRadius$ 8��._��� $��Center=�$��Center=��� -   $ NObutton��$��0 20 0 0q�$��2��$y�Hidden=�$��0��$��Нет��$��17(����	#30FFFFFF
�������$��2q�$
 Hand��
�������$��Center=�$��Center=�$	��120��$��34��#��[ ���� " ��.���  � �� �	#30FFFFFF P�  �@J�IsMouseOver .  �� .���� .���� $1 +(Button.Background).(SolidColorBrush.Color)�$ 	#45FFFFFFX�$ 0:0:0.2D� �� .���� .���� $1 +(Button.Background).(SolidColorBrush.Color)�$ 	#30FFFFFFX�$ 0:0:0.2D����� " ��.R��� #��z��#��z��#��z��$ 8��._��� $��Center=�$��Center=�