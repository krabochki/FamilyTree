   M S B A M L   `   `   `  ���� F  BFamily Tree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Family_TreeFamily_Tree      Family_Tree.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Family_Tree     -     � WindowStartupLocation$  CenterScreen=� � SizeToContent$ Manual=� � 
ResizeMode$ CanMinimize=� � Title$ Family Tree��$	��640��$	��570������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF43CCA2��� $ 	#FF195A9B�� � Offset$ 1Q�.��� $��60q�$��Center=� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� $��0��$��2�� �� $
��Auto ��� $��60 ��� $��50 �.+��� $��Assets/Images/logo.png��$	��220��$��0��$��Center=�$��Center=��� $��1��$��Family Tree��	 �� TextAlignment$	 Center=�
�������   	ExtraBold#��[  $��52(�$	��Top=�$��Center=�$��73��$	��286��$��0,4,0,0q�$��2���� $	��Top=�
 �� 
LineHeight$	
 0.8��$
��Wrap=�$��2��$*��$Прошлое в настоящем��$	 Center=�
������� 
 Medium#��[ $
��25.5(�$��Center=�$��34��$	��326���� -   $��Создать древо��  DemiBold#��[ $��25(����	#30FFFFFF
�������$��3q� � Cursor$
 Hand��$��1��
�������$��Center=�$��Center=�$	��250��$��60��$��1������  �� 
TargetType" ��.���  �� Property � �� �� Value �	#30FFFFFF �� Triggers P�  P� Property �@J�IsMouseOver P� Value .  N� EnterActions �� .���� .����  �� TargetProperty$1 +(Button.Background).(SolidColorBrush.Color)�	 �� To$ 	#45FFFFFFX� g� Duration$ 0:0:0.2D� N� ExitActions �� .���� .���� $1 +(Button.Background).(SolidColorBrush.Color)�$ 	#30FFFFFFX�$ 0:0:0.2D�����  �� 
TargetType" ��.R��� #��z��#��z��#��z�� �� CornerRadius$ 18��._��� $��Center=�$��Center=��� -   $��10q�$��Открыть древо��#��[ $��25(����	#30FFFFFF
�������$��3q�$
 Hand��
�������$��Center=�$��Center=�$��2��$	��250��$��60��$��1������ " ��.R��� #��z��#��z��#��z��$ 18��._��� $��Center=�$��Center=����� " ��.���  � �� �	#30FFFFFF P�  �@J�IsMouseOver .  �� .���� .���� $1 +(Button.Background).(SolidColorBrush.Color)�$ 	#45FFFFFFX�$ 0:0:0.2D� �� .���� .���� $1 +(Button.Background).(SolidColorBrush.Color)�$ 	#30FFFFFFX�$ 0:0:0.2D�