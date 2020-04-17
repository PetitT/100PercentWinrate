-Ajouter le script "Property Manager" à un gameobjet, cela va aussi ajouter le "Material Finder".
-Dans material finder, cliquer sur "Find Materials" pour trouver tout les matériaux utilisés dans la scène,
qui seront répertoriés dans la liste "Material Infos".
-Chaque matériel dans cette liste sera affecté par les changements de propriétés, 
sauf si la case "Affect this material" est décochée.
-Lorsque les matériels sont trouvés, leur valeur actuelle sera considérée comme leur valeur par défaut, 
le bouton "Refresh Materials defautlt values" réinitialise leur valeurs actuelles comme valeurs par défaut.
-Pour rajouter une propriété, cliquer sur l'un des boutons dans "Property Manager", 
cela va rajouter un gameobject enfant avec le script correspondant. 

--- Il est important que tout les scripts de propriétés soient enfants du gameobject possédant 
le "Property Manager et qu'il n'y ait pas deux fois la même propriété sur le même gameobject. 
Cela provoquerait des erreurs lorsque les valeurs seront animées ---

-Dans les scripts de propriétés, la variable "Name" doit correspondre au nom de la propriété dans le shader 
utilisé par le matériel.
-La valeur de la propriété peut être animée par script ou par animation.
-Il est conseillé de supprimer les matérieux non-animés dans la liste de Material Finder,
surtout quand il existe plusieurs instances de ce script.

-Pour tester les changements dans l'éditeur, il faut placer le script "Properties Preview" sur un gameObject et cliquer sur "Start Preview"
-Il faut impérativement cliquer sur Stop Preview avant de lancer le mode play, le script l'en empêche