   M S B A M L   `   `   `  ����J  FBankovni sistem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Bankovni_sistemBankovni_sistem  5   	        Bankovni_sistem.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Bankovni_sistem     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ Otvaranjebtn��5
      $��Otvaranje racuna��6'   $
��Left=�6B   $��
24,377,0,0q�6]   $	��Top=�6q   $	��189��6�   $��29��6�   6�   �� 5   
   -   5        $ 	Uplatabtn��5      $��Uplata na racun��6$   $
��Left=�6>   $��	24,10,0,0q�6Y   $	��Top=�6l   $	��189��6�   $��29��6�   6�   �� 5   
   -   5        $ Podizanjebtn��5      $��Podizanje novca��6'   $
��Left=�6A   $��	303,5,0,0q�6\   $	��Top=�6o   $	��173��6�   $��29��6�   6�   �� 5   
   -   5        $ Placanjebtn��5      $��Placanje��6&   $
��Left=�69   $��303,377,0,0q�6T   $	��Top=�6i   $	��173��6�   $��32��6�   6�   �� 5   
   -   5        $ 
Proverabtn��5      $��Provera stanja��6%   $
��Left=�6>   $��592,377,0,0q�6Y   $	��Top=�6n   $	��162��6�   $��31��6�   6�   �� 5   
   -   5         ��Name$ Korisniklbl��5      $��Trenutni korisnik ��6%   $
��Left=�6B   $��	592,8,0,0q�6]   $	��Top=�6p   $	��179��6�   �� 5   
   -   5         ��Name$ Listaprikaza��5      $��	0,44,0,62q�6T    �� View 5      �� 5      .P�5      �� $��Transaction Date��6%    �� Width$	 120��6p    �� DisplayMemberBinding 6?   �� *TransactionDate+6p   �� 5      $��Amount��6%   $ 80��6]    65   �� *Amount+6]   5      5      �� 5   
   -   5        $ Unesilbl��5      $&�� Unesite Korisnicko ime i lozinku��6"   $
��Left=�6M   $��288,114,0,0q�6h   $	��Top=�6}   $	��204��6�   �� 5   
   -	   5         ��Name$ Imetb��5      $
��Left=�6!   $��303,145,0,0q�6<    �� TextWrapping$
 Wrap=�6Q   $	��Top=�6e   $	��153��6}   �� 5   
   -
   5        $ 	Lozinkatb��5      $
��Left=�6%   $��303,180,0,0q�6@   $
 Wrap=�6U   $	��Top=�6i   $	��153��6�   �� 5   
   -   5        $ 
Prijavabtn��5      $��Button��6%   $
��Left=�66   $��330,217,0,0q�6Q   $	��Top=�6f   $	��102��6~   6�   5      5       