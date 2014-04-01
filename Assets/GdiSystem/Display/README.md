# Description
引数に渡された値を画面上に表示する。
デバッグ情報を画面に表示させたい場合などに利用できる。

渡された値はDisplayViewManagerがGUIを使って表示される。デフォルトで画面のWidth, Height, fpsが表示される。

# Usage
1. PrefabsからDisplayViewManagerを追加する
2. 表示させたい値があるスクリプト内で以下を追加する
  - usingの追加

  ````
  using GdiSystem.Display;
  ````

  - DisplayViewManager.setParameterを呼び出してパラメータを渡す

  ````
  // Example
  DisplayViewManager.SetParameter<float>("Time", Time.time);
  ````

# Samples
1. DisplayViewManager
  - Display/Samples/DisplayViewManager
