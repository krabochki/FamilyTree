   M S B A M L   `   `   `  ���� F  BFamily Tree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Family_TreeFamily_Tree      Family_Tree.TreeEditor   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Family_Tree     -   $	��400��$	��600��  � WindowStartupLocation$  CenterScreen=� � Title$. (Family Tree: Редактор древа��$	��550��$
��1100�� � 	Resources    ButtonHoverAnimationStyle �� 
TargetType �� Property �� Value �� 
TargetType � Cursor �� Padding	 J� SnapsToDevicePixels
 �� Triggers :� RoutedEvent �� TargetProperty	 �� To g� Duration  CenterGridHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%�  &	        &	 �    �� " ��.���  � ��
 �������  � �� �� " ��.R��� 
������ $
 Hand��#��z��$��0q�# z��	 . ._��� $��Center=�$��Center=�#	 z	 
 :� $ Button.MouseEnter��.[��� .���� .���� $1 +(Button.Foreground).(SolidColorBrush.Color)�$ BlackX�$ 0:0:0.2D�:� $ Button.MouseLeave��.[��� .���� .���� $1 +(Button.Foreground).(SolidColorBrush.Color)�$
 GrayX�$ 0:0:0.2D��� "  .���  � ��$ Center=�.���  � ColumnDefinitions �� $��66 �$	��186���� $
��321* �.+�o� $��2��
�������$��35��$	��Top=� K� ZIndex$ 1��$��5q�  DemiBold#��[ $��17(����� .R�]� $��Stretch=�.@�m� -   $��Персоны��$
 Hand��m� -   $��
Места��$
 Hand��m� -   $��События��$
 Hand��m� -   $��Дерево��$
 Hand��m� $��Right=�$��Прочее��$
 Hand��.@�m� -   $"��Открыть древо...�� m� Icon$
 📁��
���   �  Regular#��[ $��12(�$
 Hand��m� -   $,��&Создать проект древа��$
 📄��
���   �#��[ $��12(�$
 Hand��m� $!��Открыть пример��$
 🔍��
���   �#��[ $��12(�$
 Hand��.@�m� -   $��Романовы��$
 👑��m� -	   $��British Royal Family��$ 💂🏼��m� -
   $%��Великолепный век��$
 🕌��m� -   $��Справка��$	   ?��
���   �#��[ $��12(�$
 Hand��m� -   $��Разработчик��$
 🔧��
���   �#��[ $��12(�$
 Hand��m� -   $��
Выход��$
  ✕��
���   �#��[ $��12(�$
 Hand������  �� EndPoint$ 0.5,0&� �� 
StartPoint$ 0,0.5&�.���  � Color$ 	#FF43CCA2��� $ 	#FF195A9B�� � Offset$ 1Q�� $��1�����#f1f1f1.+�&   System.Windows.Controls.DataGrid  -     Name$ 
myDataGrid��$��1��"   VerticalScrollBarVisibility$
 Auto=���.    RowBackground �#f1f1f1   GridLinesVisibility$
 None=�   AlternatingRowBackground �#e8e8e8$
��Auto��$��16(�$
��Left=�$��0q�   
IsReadOnly .    CanUserResizeColumns .     CanUserResizeRows .     AutoGenerateColumns .  	 Light#��[ ���	#FFF1F1F1$��0 35 0 0q� ��%Q   (��       * SystemColors.HighlightBrushKey+)�� .��� $ #1a5d9b��$ 1Q�   ColumnHeaderStyle �� "  .���  � ��$ Center=�    	CellStyle  �� *  $System.Windows.Controls.DataGridCell"  .���  � ��$	 0.3q���  � ��
 �����!   Columns! � $��0��$��0,25,0,0q� �� $
��139* �" � RowDefinitions" �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -   # ��Name$
# rect��$��1��
�������$��5��$��	0,20,0,20q�$ J� RenderTransformOrigin$$ 0.5,0.5&�z�V� .~��� �� % �� AngleY$% 0.007Q��� T� & T� Y$& 0.012Q��� -   $��0��$��Добавить��$��	10,25,0,0q�  IconButtonStyle#��[ 
' � Tag$!' Assets/Images/Icons/add.png��$
 Hand���� -   $��1��$��Удалить��$��10,0,0,0q�#��[ $$' Assets/Images/Icons/delete.png���� -   $��2��$��Изменить��$��10,0,0,0q�#��[ $"' Assets/Images/Icons/edit.png���� -   $��3��$��Показать��$��10,0,0,0q�#��[ $"' Assets/Images/Icons/view.png��$��35��$��Center=��� -   ( ��Name$( sortButton1��$��6��$��
Живые��$��10,0,0,0q�#��[ $$' Assets/Images/Icons/noicon.png��$��35��$��Center=��� -   $( sortButton2��$��7��$��Взрослые��$��10,0,0,0q�#��[ $$' Assets/Images/Icons/noicon.png��$��35��$��Center=��� -   $( sortButton3��$��8��$��Есть фото��$��	10,0,0,30q�#��[ $$' Assets/Images/Icons/noicon.png��$��35��$	��Top=�