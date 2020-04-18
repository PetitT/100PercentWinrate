-Ce package contient des scripts permettant d'animer les valeurs du post process.
-Il contient actuellement un contrôleur de LiftGammaGain, ColorAdjustment, Chromatic Aberration et Bloom.
-Pour s'en servir, il suffit de placer l'un de ces scripts sur un gameobject et de référencer le 
post process volume. Seules les valeurs activées dans le post process seront animées.

-Ce package dispose d'outils utilisant ces contrôleurs.

-Le script "ColorFadeInOut" utilise le ColorAdjustment pour faire un fade de l'écran.
-Pour utiliser le script "ColorFadeInOut", l'attacher à un gameObject et référencer un "Color Adjustment Controller"
