   M S B A M L   `   `   `  ����O  KWpfMailDBManipulator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullU/clr-namespace:WpfMailDBManager.MainTableWindows!WpfMailDBManager.MainTableWindows  5   	   ?    9WpfMailDBManager.MainTableWindows.ShowOrDeleteInMainTable   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:WpfMailDBManager.MainTableWindows     5   	     � Title$F  @Просмотр записи из Главной таблицы��5   	   $	��400��64   $	��450��6A   $	��400��6M   $	��450��6]   .��5	      � .+�5
   
   � $
��Left=�6   $��	3,10,0,35q�6*   $	��119��6=   .+�5      �� -   5         ��Name$$ SODMainTable_TBl_lable_mail_Id��5      $
��Left=�6@   $��	10,10,0,0q�6[   $
��Wrap=�6n   $��Индекс��6�   $	��Top=�6�   �� 5      -   5        $) #SODMainTable_TBl_lable_id_Operation��5      $
��Left=�6E   $��	10,31,0,0q�6`   $
��Wrap=�6s   $��Операция��6�   $	��Top=�6�   �� 5      -   5        $' !SODMainTable_TBl_lable_id_Mailing��5      $
��Left=�6C   $��	10,52,0,0q�6^   $
��Wrap=�6q   $��Тип��6�   $	��Top=�6�   �� 5      -   5        $, &SODMainTable_TBl_lable_id_Subscription��5      $
��Left=�6H   $��	10,73,0,0q�6c   $
��Wrap=�6v   $"��Инфо о подписке��6�   $	��Top=�6�   �� 5      -   5        $+ %SODMainTable_TBl_lable_sender_Address��5      $
��Left=�6G   $��
10,0,0,165q�6b   $
��Wrap=�6v   $'��!Адрес отправителя��6�   $��Bottom=�6�   �� 5      -   5        $. (SODMainTable_TBl_lable_recipient_Address��5      $
��Left=�6J   $��
10,0,0,113q�6e   $
��Wrap=�6y   $%��Адрес получателя��6�   $��15.96��6�   $��Bottom=�6�   �� 5      -   5        $+ %SODMainTable_TBl_lable_weight_Package��5      $
��Left=�6G   $��	10,0,0,60q�6b   $
��Wrap=�6u   $ ��Вес пакета (кг)��6�   $��Bottom=�6�   �� 5      -   5        $+ %SODMainTable_TBl_lable_date_Operation��5      $
��Left=�6G   $��	10,0,0,39q�6b   $
��Wrap=�6u   $��Дата операции��6�   $��Bottom=�6�   �� 5      -	   5        $" SODMainTable_TBl_lable_price��5      $
��Left=�6>   $��9,0,0,18q�6Y   $
��Wrap=�6k   $��Цена (грн)��6   $��16��6�   $��Bottom=�6�   5      �� 5   
   
���   �$��1q�6%   $
��Left=�69   $��0��6T   $��127,119,0,0q�6_   $	��Top=�6t   $��7��6�   � 5   
   $��124,10,10,35q�6   .+�5      �� -
   5         ��Name$ SODMainTable_TBox_mail_id��5      $��
10,10,10,0q�69    �� TextWrapping$
 Wrap=�6M   $�� ��6a   $	��Top=�6i   ���#EEEEEE �� 
IsReadOnly . 6�   �� 5      -   5        $$ SODMainTable_TBox_id_Operation��5      $��
10,31,10,0q�6>   $
 Wrap=�6R   $�� ��6f   $	��Top=�6n   ���#EEEEEE . 6�   �� 5      -   5        $" SODMainTable_TBox_id_Mailing��5      $��
10,52,10,0q�6<   $
 Wrap=�6P   $�� ��6d   $	��Top=�6l   ���#EEEEEE . 6�   �� 5      -   5        $' !SODMainTable_TBox_id_Subscription��5      $��10,73,10,187q�6A   $
 Wrap=�6W   $�� ��6k   ���#EEEEEE . " �� VerticalScrollBarVisibility$
 Auto=�6�   �� 5      -   5        $&  SODMainTable_TBox_sender_Address��5      $��10,0,10,134q�6@   $
 Wrap=�6U   $�� ��6i   ���#EEEEEE . $��48��6�   $��Bottom=�6�   $
 Auto=�6�   �� 5      -   5        $) #SODMainTable_TBox_recipient_Address��5      $��
10,0,10,81q�6C   $
 Wrap=�6W   $�� ��6k   ���#EEEEEE . $��48��6�   $��Bottom=�6�   $
 Auto=�6�   �� 5      -   5        $&  SODMainTable_TBox_weight_Package��5      $��
10,0,10,58q�6@   $
 Wrap=�6T   $�� ��6h   $��Bottom=�6p   ���#EEEEEE . 6�   �� 5      -   5        $&  SODMainTable_TBox_date_Operation��5      $��
10,0,10,37q�6@   $
 Wrap=�6T   $�� ��6h   $��Bottom=�6p   ���#EEEEEE . 6�   �� 5      -   5        $ SODMainTable_TBox_price��5      $��
10,0,10,16q�67   $
 Wrap=�6K   $�� ��6_   $��Bottom=�6g   ���#EEEEEE . 6�   5       � 5!   
    � ColumnDefinitions 5"      �� 5#      6%   �� 5$      6%   .+�5&      �� -   5        $ textBlock_OKInput��5&      $��1��63   $��	15,0,0,35q�6C   {�.  $y�Visible=�6h   $
��Wrap=�6}   $D��>Для удаления записи нажмите 2 раза��6�   $��16��6�   $��Bottom=�6�   $��10(�6�   $
��Left=�6�   $	��174��6  �� 5'      -   5         ��Name$ button_OKInput��5'      $��1��6-   {�.  $y�Visible=�6O    �� 	IsDefault . $��OK��6u   $��	50,0,0,15q�6�   $
��Left=�6�   $	��100��6�   $��20��6�   $��Bottom=�6�   �� 5(      -   5        $ button_CancelInput��5(      $��0��61   {�.  $y�Visible=�6S   	 �� IsCancel	 . $��Отмена��6x   $��	0,0,50,15q�6�   $��Right=�6�   $	��100��6�   $��20��6�   $��Bottom=�6�   5)      �� 5*   
   -   5        $ 	button_OK��5*      $��0��6$   {�. $y�Visible=�6E    . 	 . $��OK��6{   $��Center=�6�   $	��100��6�   $��20��6�   $��Bottom=�6�   $��171,0,171,15q�6�   $��2��6  5+      5,      