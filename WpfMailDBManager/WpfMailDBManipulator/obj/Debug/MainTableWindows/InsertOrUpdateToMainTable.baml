   M S B A M L   `   `   `  ����O  KWpfMailDBManipulator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:WpfMailDBManager.MainTableWindows!WpfMailDBManager.MainTableWindows  5   	   A    ;WpfMailDBManager.MainTableWindows.InsertOrUpdateToMainTable   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:WpfMailDBManager.MainTableWindows     5   	     � Title$H  BДобавление записи в Главную таблицу��5   	   $	��350��65   $	��450��6B   $	��350��6N   $	��350��6^   .��5	      � .+�5
   
   � $
��Left=�6   $��	3,10,0,33q�6*   $	��119��6=   .+�5      �� -   5         ��Name$) #IOUMainTable_TBl_lable_id_Operation��5      $
��Left=�6E   $��	10,12,0,0q�6`   $
��Wrap=�6s   $��Операция��6�   $	��Top=�6�   �� 5      -   5        $' !IOUMainTable_TBl_lable_id_Mailing��5      $
��Left=�6C   $��	13,39,0,0q�6^   $
��Wrap=�6q   $��Тип��6�   $	��Top=�6�   �� 5      -   5        $, &IOUMainTable_TBl_lable_id_Subscription��5      $
��Left=�6H   $��	10,66,0,0q�6c   $
��Wrap=�6v   $"��Инфо о подписке��6�   $	��Top=�6�   � 5      $��	0,87,0,78q�6    � RowDefinitions 5      �� 5      $��* �6$   61   �� 5      $��* �6$   61   .+�5      �� -   5        $+ %IOUMainTable_TBl_lable_sender_Address��5      $��0��6L   $
��Left=�6Y   $��	10,10,0,0q�6t   $
��Wrap=�6�   $'��!Адрес отправителя��6�   $	��Top=�6�   �� 5      -   5        $. (IOUMainTable_TBl_lable_recipient_Address��5      $��1��6N   $
��Left=�6[   $��	10,10,0,0q�6v   $
��Wrap=�6�   $%��Адрес получателя��6�   $��16��6�   $	��Top=�6�   5      �� 5      -   5        $+ %IOUMainTable_TBl_lable_weight_Package��5      $
��Left=�6G   $��	10,0,0,62q�6b   $
��Wrap=�6u   $ ��Вес пакета (кг)��6�   $��16��6�   $��Bottom=�6�   �� 5      -   5        $+ %IOUMainTable_TBl_lable_date_Operation��5      $
��Left=�6G   $��	10,0,0,41q�6b   $
��Wrap=�6u   $��Дата операции��6�   $��16��6�   $��Bottom=�6�   �� 5      -   5        $" IOUMainTable_TBl_lable_price��5      $
��Left=�6>   $��	10,0,0,20q�6Y   $
��Wrap=�6l   $��Цена (грн)��6�   $��16��6�   $��Bottom=�6�   5      �� 5   
   
���   �$��1q�6%   $
��Left=�69   $��0��6T   $��127,119,0,0q�6_   $	��Top=�6t   $��7��6�   � 5   
   $��124,10,10,33q�6   .+�5      �� -	   5         ��Name$( "IOUMainTable_ComboBox_id_Operation��5      $��
10,10,10,0q�6D    �� Text$  ��6X   $	��Top=�6`   �� 5      -
   5        $&  IOUMainTable_ComboBox_id_Mailing��5      $��
10,37,10,0q�6A   $  ��6U   $	��Top=�6]   �� 5      -   5        $+ %IOUMainTable_ComboBox_id_Subscription��5      $��
10,64,10,0q�6F   $  ��6Z   ���#EEEEEE$��22��6w    �� 
IsEditable .  $	��Top=�6�    �� StaysOpenOnEdit . 6�   � 5      $��	0,86,0,79q�6    5       �� 5!      $��* �6$   61   �� 5"      $��* �6$   61   .+�5$      �� -   5         ��Name$&  IOUMainTable_TBox_sender_Address��5$      $��0��6D   $��	10,5,10,3q�6Q    �� TextWrapping$
 Wrap=�6d   $�� ��6x   ���#EEEEEE"	 �� VerticalScrollBarVisibility$
	 Auto=�6�   �� 5%      -   5        $) #IOUMainTable_TBox_recipient_Address��5%      $��1��6G   $��	10,3,10,4q�6T   $
 Wrap=�6g   $�� ��6{   ���#EEEEEE$
	 Auto=�6�   5&      �� 5'      -   5        $&  IOUMainTable_TBox_weight_Package��5'      $��
10,0,10,61q�6^   $
 Wrap=�6r   $�� ��6�   ���#EEEEEE$��18��6�   $��Bottom=�6�   �� 5(      -   5        $&  IOUMainTable_TBox_date_Operation��5(      $��
10,0,10,38q�6@   $
 Wrap=�6T   $�� ��6h   ���#EEEEEE{�.  $��18��6�   $��Bottom=�6�   �� 5)      -   5        $ IOUMainTable_TBox_price��5)      $��
10,0,10,15q�6U   $
 Wrap=�6i   $�� ��6}   ���#EEEEEE$��18��6�   $��Bottom=�6�   5*      � 5+   
   
 � ColumnDefinitions
 5,      �� 5-      6%   �� 5.      6%   .+�50      �� -   5         ��Name$ IOUMainTable_button_OK��50      $��1��65   $��OK��6E   $��	50,0,0,15q�6R   $
��Left=�6}   $	��100��6�   $��20��6�   $��Bottom=�6�   �� 51      -   5        $  IOUMainTable_button_Cancel��51      $��0��69   $��Отмена��6I   $��	0,0,50,15q�6Z   $��Right=�6�   $	��100��6�   $��20��6�   $��Bottom=�6�   52      53      54      