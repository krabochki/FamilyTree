   M S B A M L   `   `   `  ���� F  BFamily Tree, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Family_TreeFamily_Tree      Family_Tree.App   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     +localclr-namespace:Family_Tree       �� 	Resources     Bold  DemiBold  	ExtraBold 	 Light 
 Medium  Regular 
 Italic �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name  Icon � ColumnDefinitions ��Name	 J� Opacity
 �� RelativeSource ��Name �� Triggers P� Property P� Value N� EnterActions �� 
TargetName �� TargetProperty R� KeyTime R� Value N� ExitActions  ToolTipStyle 	 IconButtonStyle	 W� To g� Duration 
 ComboBoxTextBoxStyle � Cursor �� 	ClickMode d� 	IsChecked �� Path �� Mode ��Name F�Name �� SelectionBoxItem �� SelectionBoxItemTemplate  �� IsDropDownOpen! � AllowsTransparency" J� SnapsToDevicePixels# � ActualWidth$ �� MaxDropDownHeight% K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation&   DirectionalNavigation  GradientButton3  -System.Windows.Media.Effects.DropShadowEffect'   
BlurRadius(   Opacity)   ShadowDepth* �� From	+ �� To, �� 
StartPoint- �� EndPoint. � Color/ � Offset  
JustButton  GradientTextBlock��%�  &	        &	 W     &	 �     &	     &	 p    &	 �    &	      '�� }    &	 t    &	 }    7  &		 �    7  &	
 �    '�� "	    7 
 &	 [    7   &	 �    7  &	 �    7   *�QM/Family Tree;component/Assets/Fonts/VisbyRoundCF-Bold.ttf#Visby Round CF Bold)�*�YU/Family Tree;component/Assets/Fonts/VisbyRoundCF-DemiBold.ttf#Visby Round CF DemiBold)�*�[W/Family Tree;component/Assets/Fonts/VisbyRoundCF-ExtraBold.ttf#Visby Round CF ExtraBold)�*�SO/Family Tree;component/Assets/Fonts/VisbyRoundCF-Light.ttf#Visby Round CF Light)�*�UQ/Family Tree;component/Assets/Fonts/VisbyRoundCF-Medium.ttf#Visby Round CF Medium)�*�OK/Family Tree;component/Assets/Fonts/VisbyRoundCF-Regular.ttf#Visby Round CF)�*�WS/Family Tree;component/Assets/Fonts/VisbyRoundCF-Oblique.ttf#Visby Round CF Oblique)��� " ��.���  � ��$ Italic$���  � ��$
 16.5����  �@��TextWrapping$
 Wrap=��� # �@��VerticalScrollBarVisibility$
 Auto=���  � �� �� " ��.R��� #��z��#��z��#��z��$ 3��._��� $ PART_ContentHost���� " d�.���  � �� �� " d�.R��  �� $
��Auto ��� $
��Auto �.+��� $
 icon��$��Center=�$��Center=�$��24��$��24��$		 0.5Q�$��0,0,10,0q����� *Tag+
 �� *TemplatedParent+�� $ contentPresenter��$��1��$
��Left=�$��Center=�$		 0.5Q� P�  �@d�	IsChecked$
 TrueY� �� .���� .��T� $
 icon��$ Source�..�i� $ 0��  Assets/Images/Icons/view.png �� .���� .��T� $
 icon��$ Source�..�i� $ 0�� Assets/Images/Icons/add.png�� " _�.���  � ��8   ��  � ��$ 12(��� " ��.���  �@�Cursor$
 Hand����  � �� �	#80000000��  � ��$
 Left=���  � ��$ 35����  � ��$ 20(���  � ��8   ��  � �� �� " ��.R��  �� $
��Auto ��� $
��Auto �.+��� $
 icon��$��Center=�$��Center=�$��24��$��24��$		 0.5Q�$��0,0,10,0q����� *Tag+
 �� *TemplatedParent+�� $ contentPresenter��$��1��$
��Left=�$��Center=�$		 0.5Q� P�  �@J�IsMouseOver .  �� .���� .��W� $
 icon��$ Opacity�$ 1X�$ 0:0:0.3D�W� $ contentPresenter��$ Opacity�$ 1X�$ 0:0:0.3D� �� .���� .��W� $
 icon��$ Opacity�$	 0.5X�$ 0:0:0.3D�W� $ contentPresenter��$ Opacity�$	 0.5X�$ 0:0:0.3D��� " ��.���  � �� �� " ��.R�� .+��� $ 4,0,0,4��$��1q�#��z�����	#FFABADB3._��� $ PART_ContentHost���� " ��.���  �@��
IsEditable .  ��  � �� �� " ��.R��  �� �� $��20��.+��� $��PART_EditableTextBox��8��  $��4,0,0,0q�#��z��d� $��1��$��0q�#��z��|�.  ���	#FFABADB3$
 Hand��$ Press=� �� $ IsDropDownOpen�$ TwoWay=�
 �� *TemplatedParent+���� " d�.R�� .+��� $ Border��#��z��#��z��$��0 1 1 1q�$ 0 4 4 0��._��� F� $ Arrow��$��1��$��Center=�$��Center=����	#FFABADB3"����        � 	=  	= � z     	�� $��ContentSite��#��z #��z #��z��$��Center=�$
��Left=�$��5,0,0,0q�� $��Popup��$��Bottom=�#��z  ! . |�.  $��Slide=�.��� $��DropDown��" . #��z# #��z$ .+��� $ DropDownBorder��$��1q�$ 5��
����������	#FFABADB3�� $��4,6,4,6q�" . .���� % . $& 	Contained=��� " ��.���  �@�Cursor$
 Hand����  � ��$ 3q���  � ��8   ��  � ��
 �������  � ��
 �������  � ��$ 18(���  � ��$	 170����  � ��$ 48����  �@J�Effect   $' 12Q�$
( 0.25Q�$) 4Q���  � �� �� " ��.R��� $ border��#��z��
�������#��z��$ 12��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver .  �� .���� .���� $* 	#FF43CCA2X�$+ 	#FF195A9BX�$ 0:0:0.2D�$ border��$@ :(Border.Background).(GradientBrush.GradientStops)[0].Color��� $* 	#FF195A9BX�$+ 	#FF43CCA2X�$ 0:0:0.2D�$ border��$@ :(Border.Background).(GradientBrush.GradientStops)[1].Color� �� .���� .���� $* 	#FF195A9BX�$+ 	#FF43CCA2X�$ 0:0:0.2D�$ border��$@ :(Border.Background).(GradientBrush.GradientStops)[0].Color��� $* 	#FF43CCA2X�$+ 	#FF195A9BX�$ 0:0:0.2D�$ border��$@ :(Border.Background).(GradientBrush.GradientStops)[1].Color���  � �� �� $, 0,0.5&�$- 1,0.5&�.��� $. 	#FF43CCA2��� $. 	#FF195A9B��$/ 1Q��� " ��.���  � ��
 ���� ��  � �� �#717171��  �@�Cursor$
 Hand����  � ��8   ��  � ��$ 5 5 0 5q���  � ��$	 100����  � �� �� " ��.R��� $ border��#��z�����#717171#��z��$ 5��._��� $��Center=�$��Center=��� " ��.���  � ��$
 Bold"���  �@��Padding$ 5 0 0 0q���  � ��$ Center=���  � ��$	 246����  � ��$	 Top=���  � ��8   ��  � ��$ 16(���  � ��$ 22����  � ��
 �������  � �� �� $, 0,0.5&�$- 1,0.5&�.��� $. 	#FF43CCA2��� $. 	#FF195A9B��$/ 1Q�