   M S B A M L   `   `   `  ���� F  BFamily Tree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Family_TreeFamily_Tree      Family_Tree.ShowTree   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Family_Tree       � WindowStartupLocation$  CenterScreen=� � Title$. (Family tree: Просмотр древа��$	��700��$	��900�� � 	Resources    HoverButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType��%q   &	        �� " ��.���  � �� �� " ��.R��� #��z��#��z��#��z��._��� $��Center=�$��Center=�.���  � RowDefinitions �� $��* ��� $
��Auto �.+�]� .+��� -    ��Name$ slider��	 � Minimum$		 0.1Q�
 � Maximum$
 3Q�$��Right=� � Value$	 0.1Q� �� Orientation$ Vertical=��� $��Visible=�$��Visible=�$��RightToLeft=���. $
��Left=�.���� -    ��Name$ imageviewer��$��Assets/Images/woman.png��$��40q��� $��Right=�$	��Top=�$��	0,18,30,0q�$��45��$��45��$��0 0 0 5q�.   (Family_Tree.ContextMenuLeftClickBehavior   IsLeftClickEnabled .  � Cursor$
 Hand��$��0��$	��☰��
�������
������ 
������ $��0q�  Regular#��[ $��35(� � ContextMenu �� .@�m� -   $#��Сохранить древо�� m� Icon$
 📁��#��[ $
 Hand��m� -   $#��Настроить снова��$
  ↺��#��[ $
 Hand��m� -   $��
Выйти��$
  ✕��#��[ $
 Hand��