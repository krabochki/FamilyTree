   M S B A M L   `   `   `  ����F  BFamily Tree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Family_TreeFamily_Tree  5   	       Family_Tree.ShowTree   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Family_Tree     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$. (Family tree: Просмотр древа��5	   	   $	��700��5
   	   $	��900��6    � 	Resources 5         HoverButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType��%�   &	        �� 5   
   " ��6)   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   ._�5      �� $��Center=�6/   $��Center=�5   /   5      5      5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5"   
   ]� .+�5#      �� -   5         ��Name$ slider��5#   :   	 � Minimum$		 0.1Q�6J   
 � Maximum$
 3Q�6X   $��Right=�6e    � Value$	 0.1Q�6|    �� Orientation$ Vertical=�6�   6�   �� 5$      $��Visible=�6   $��Visible=�6B   $��RightToLeft=�6j   ��. $
��Left=�6�   .��5%      �� -   5         ��Name$ imageviewer��5%      $��Assets/Images/woman.png��6.   $��40q�6Q   6_   5'      5*      �� 5,   
   $��Right=�6   $	��Top=�6-   $��	0,18,30,0q�6E   $��45��6X   $��45��6d   $��0 0 0 5q�6o   .   (Family_Tree.ContextMenuLeftClickBehavior   IsLeftClickEnabled .  � Cursor$
 Hand��6�   $��0��6�   $	��☰��6�   
�������
������ 
������ $��0q�6,    Regular#��[ 6@  $��35(�6f   � ContextMenu 5-      �� 5.      .@�5/      m� -   5        $#��Сохранить древо��5/       m� Icon$
 📁��68   #��[ 6Z   $
 Hand��6�   m� 50      -   5        $#��Настроить снова��50      $
  ↺��68   #��[ 6\   $
 Hand��6�   m� 51      -   5        $��
Выйти��51      $
  ✕��6.   #��[ 6R   $
 Hand��6x   52      54      56      57      