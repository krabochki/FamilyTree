   M S B A M L   `   `   `  ���� F  BFamily Tree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Family_TreeFamily_Tree      Family_Tree.AddEditPlace   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Family_Tree       � Title$?  9Family Tree: Добавление нового места��$	��347��$	��850�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=����	#FFF1F1F1.��� $��10q� � ColumnDefinitions �� �� �� $	��280 �.+�� $��0��$��1��$��0 10 0 0q� � RowDefinitions �� $��33 ��� $
��Auto �.+��� $��Полное имя��   GradientTextBlock#��[  �� $	��246��$��1��.+��� $��Название:�� 
 Medium#��[ �� -    ��Name$ name_textbox�� �� 	MaxLength$ 50�� 
 Italic#��[ $��0 0 0 4q��� $��Сокращение:��#��[ �� -   $ abbr_textbox��$ 25��#��[ $��0 0 0 4q��� $0��*Историческое название:��#��[ �� -   $ historicalname_textbox��$ 50��#��[ �� $��23��$��	0 15 0 15q�$'��!Прочая информация��#��[  �� $��1��$	��246��.+��� $��Тип:��#��[ �� -   $ typeTextBox��$ 25��$��0 0 0 4q�#��[ �� $��Адрес:��#��[ �� -   $ addressTextBox��$��36��$	 100��#��[ � $��0��$��2��$��0 10 5 0q� �� $��33 ��� $
��Auto �.+��� $��Примечания��#��[  �� $	��246��$��1��.+��� $��Описание:��#��[ �� -   $ descrTextBox��$	 500��#��[ $��36��$��0 0 0 4q��� $��Комментарий:��#��[ �� -   $ placeNotesTextBox��$	 300��$��Italic$�#��[ $��36��� $��Center=�$��Center=�$��1��$��0��$��2��$	��120��$��	0 125 0 0q� �� �� .+��� -   $��0��$��Сохранить��  GradientButton#��[  � ToolTip _�   ToolTipStyle#��[ $N��HСохранить событие с введёнными данными���� -	   $��Отменить��#��[ $��1�� � Cursor$
 Hand�� _� #��[ $Z��TЗакрыть форму без сохранения данных о событии��� $��0��$��0��$��2��$��Center=�.+��� -
   	 ��Name$	 place_Photo��$��UniformToFill=�$��0��$��0��$	��220��$	��220��$��2��$��Assets/Images/place.png��$��2���� $��Right=�$	��Top=�$��	0,20,35,0q�$��30��$��30��.   (Family_Tree.ContextMenuLeftClickBehavior
   IsLeftClickEnabled
 . $
 Hand��$��0��$	��☰��
�������
������ $��0q�  Regular#��[ $��24(� � ContextMenu �� .@�m� -   $ ��Выбрать фото...�� m� Icon$
 📁��#��[ $
 Hand��m� -   $!��Нет фотографии��$
  ✕��#��[ $
 Hand��