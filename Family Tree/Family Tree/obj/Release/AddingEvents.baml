   M S B A M L   `   `   `  ���� F  BFamily Tree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Family_TreeFamily_Tree      Family_Tree.AddEditEvent   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     +localclr-namespace:Family_Tree       � Title$6  0Добавление нового события��$	��364��$	��830�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=����	#FFF1F1F1.��� $��10q� � RowDefinitions ��  � ColumnDefinitions �� �� �� $	��280 �.+�� $��0��$��0��$��Center=�.+��� -    ��Name$ 
eventPhoto��$��UniformToFill=�$��0��$��0��$	��220��$	��220��$��2��$��Assets/Images/event.png���� $��Right=�$	��Top=�$��	0,18,30,0q�$��30��$��30��.   (Family_Tree.ContextMenuLeftClickBehavior   IsLeftClickEnabled .  � Cursor$
 Hand��$��0��$	��☰��
�������
������ $��0q�   Regular#��[  $��24(� � ContextMenu �� .@�m� -   $ ��Выбрать фото...��	 m� Icon$
	 📁��#��[  $
 Hand��m� -   $!��Нет фотографии��$
	  ✕��#��[  $
 Hand��� $��0��$��1��$��0,10,0,0q� �� $��33 ��� $
��Auto �.+��� $+��%Основная информация��$��0��  GradientTextBlock#��[ �� $	��246��$��1��.+��� $��Тип:�� 
 Medium#��[ �� -   
 ��Name$
 typeComboBox��$��0 0 0 4q� 
 Italic#��[ $	��5 0q�$��Italic$� �� 
IsEditable .  �� 
IsReadOnly . $��Center=�.@��� $�� ���� $��Свадьба���� $��Брак���� $��Развод���� $��Путешествие���� $��Переезд���� $��Расставание���� $��Выпускной���� $��Повышение���� $��Свидание���� $%��Начало отношений���� $��Поступление���� $��Увольнение���� $*��$Первый рабочий день���� $)��#Первый день в школе���� $��Gender party���� $��Поминки���� $��	Дата:��#��[ (  "System.Windows.Controls.DatePicker  -     Name$ datePickerOfEvent��$��0 0 0 4q�$��Italic$�#��[ $��25�����	#FFABADB3
�������$��	5,2.5,5,0q� � 	Resources  �� 
TargetType �� Property �� Value:  4System.Windows.Controls.Primitives.DatePickerTextBox��%j   '��       '  /     �� " ��.���  �@��CornerRadius$ 4���� "  .���  � ��$ Center=��� .+��� $(��"Участники события:��#��[ �� -   $
 participantsComboBox��$	��5 0q�$��Italic$�#��[ $��0 0 0 4q� .  . $��Center=��� $"��Роль участника:��#��[ �� -   $
 roleTextBox��$	��5 0q�$��Italic$�#��[  �� IsTextSearchEnabled .  ��  �� Orientation$ 
Horizontal=�$��Center=�$��30��$��0 3 0 3q�.+��� -    ��Name$ buttonAddParticipant��$��Добавить��  
JustButton#��[ �� -	   $ buttonDeleteParticipant��$��Удалить��#��[ �� -
    ��Name$ textBox��
�������$��0 0 0 7q�#��[ $��36�� �� 
IsReadOnly . 
��������� $��Место:��#��[ �� -   $
 placesComboBox��$��0 0 0 5q�$	��5 0q�$��Italic$�#��[  .  . $��Center=�� $��0��$��3��$��0,10,5,0q� �� $��33 ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Примечания��#��[ $��0���� $	��246��$��1��.+��� $��Описание:��#��[ �� -   $ descriptiontextbox�� �� 	MaxLength$	 500��#��[ $��54��$��0 0 0 4q��� $��Комментарий:��#��[ �� -   $ notesTextBox��$	 300��$��Italic$�#��[ $��36��� $��Center=�$��Center=�$��1��$��0��$��2��$	��120��$��	0 140 0 0q� �� �� .+��� -   $��0��$��Сохранить��  GradientButton#��[  � ToolTip _�   ToolTipStyle#��[ $N��HСохранить событие с введёнными данными���� -   $��Отменить��#��[ $��1�� _� #��[ $Z��TЗакрыть форму без сохранения данных о событии��