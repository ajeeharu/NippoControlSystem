# NippoControlSystem (配線チェッカー・測定制御システム)

本プロジェクトは、配線検査・検査データ入力・測定機器制御を行う Windows デスクトップアプリケーションです。  
オニオンアーキテクチャおよび MVVM パターンをベースに設計されており、ハードウェア制御 (DIO/AIO) や Win32 API 制御、ファイル I/O といったインフラ層の処理をドメイン・UI 表現から分離した構成となっています。

---

## 📁 ディレクトリ・フォルダ構成

```text
NippoControlSystem/
│
├── UI/                                  # 【Presentation Layer】画面および表示ロジック
│   ├── Views/                           # XAML / WinForms 画面レイアウト
│   ├── ViewModels/                      # MVVM ViewModel クラス群
│   │   ├── DataInputViewModel.cs        # 検査データ入力画面のロジック
│   │   └── SerialViewModel.cs           # シリアル通信・機器測定画面のロジック
│   ├── Controls/                        # カスタムコントロール・UIコンポーネント
│   ├── Properties/                      # リソース・アセンブリ情報等
│   ├── Program.cs                       # アプリケーションのエントリーポイント
│   └── DependencyInjection.cs           # DI（依存性注入）コンテナの設定・構成
│
├── ApplicationService/                  # 【Application Layer】ユースケース・アプリケーションサービス
│   ├── Dtos/                            # データ転送オブジェクト (DTO)
│   ├── Interfaces/                      # アプリケーションサービスのインターフェース
│   ├── Scenarios/                       # ユースケース・シナリオ実行ロジック
│   └── Services/                        # アプリケーションサービス実装
│
├── Domain/                              # 【Domain Layer】ビジネスロジックおよびコア抽象
│   ├── Models/                          # ドメインデータモデル
│   │   └── MeasureCondition.cs          # 測定条件・検査項目モデル
│   ├── Interfaces/                      # ハードウェア・サービス抽象化インターフェース
│   │   └── ICaioNative.cs               # AIO/DIO制御用のネイティブインターフェース (enum定義含む)
│   └── Services/                        # ドメインサービス（ドメイン固有のロジック・計算等）
│
└── Infrastructure/                      # 【Infrastructure Layer】外部連携・ファイル/ハード制御
    ├── Configuration/                   # アプリケーション設定・永続化
    │   └── Settings.cs (Cyc.IO)         # XML/App.config 設定値保持・ファイル入出力
    ├── Devices/                         # 測定器・ハードウェアデバイス制御実装
    ├── Mocks/                           # テスト・デモ用モックデバイス/サービス実装
    ├── NativeLibs/                      # 外部 DLL ライブラリ連携
    │   └── CaioNativeLib.cs             # コンテック AIO API (caio.dll) 実装
    ├── Persistence/                     # データベース・ファイル保存・データ変換処理
    │   └── MeasureConditionCNV.cs       # 検査条件ファイルの旧/新文字列変換・テキスト置換
    ├── Services/                        # ログ出力・インフラレベルのサービス
    └── Win32/                           # Win32 API / P/Invoke 処理
        └── Win32Api.cs                  # FindWindow 等の P/Invoke 定義
```

---

## 🏛 レイヤー構造と各モジュールの解説

* **`DataInputViewModel.cs`**  
  検査項目の入力・切り替え（`StartTNo` / `TNo`）、DataSet へのデータ復元（`RestoreCurrentData`）やデータ読み込みを担当する ViewModel。
* **`SerialViewModel.cs`**  
  測定・シリアルナンバー等の検査制御を行う ViewModel。
* **`screenShot.cs` (`Cyc.Windows.Forms`)**  
  `Form` や `Control` の画面イメージをキャプチャし、印刷ダイアログやプレビュー表示を行なう UI ユーティリティ。

### 1. UI Layer (Presentation 層)

画面描画およびユーザーインターフェースとの対話を担う層です。表示制御およびユーザー操作の受け付けに専念します。

Views/: 画面レイアウト・UIデザイン定義。

* **`ViewModels/`**  :
MVVMパターンに基づく表示ロジック・状態保持。

* **`DataInputViewModel.cs`**  :
検査項目の入力・切り替え（StartTNo / TNo）、DataSet へのデータ復元（RestoreCurrentData）やデータ読み込みを担当。

* **`SerialViewModel.cs`**  :
 測定・シリアルナンバー等の検査制御を担当。

* **`Controls/`**  :
 再利用可能なカスタムコントロールおよび表示用UIコンポーネント。

* **`Properties/`**  :
 アプリケーションリソースやアセンブリ情報。

* **`Program.cs`**  :
 アプリケーションの開始・エントリーポイント。

* **`DependencyInjection.cs`**  :
 DIコンテナの設定。各層のインターフェースと実装クラスのバインド・ライフサイクル管理を担当。

---

### 2. Application Layer (アプリケーションサービス層)

ユースケースの実現、ドメインモデルとUI層の連携仲介、データ転送を行います。

※ .NET標準の System.Windows.Forms.Application や System.Windows.Application クラスとの名前空間衝突を回避するため、フォルダー・レイヤー名を ApplicationService としています。

* **`Dtos/`**  : 
UI層とApplication層間でデータをやり取りするための軽量オブジェクト。

* **`Interfaces/`**  :
 ユースケースやアプリケーションサービスの抽象インターフェース定義。

* **`Scenarios/`**  : 
一連の検査手順や測定シーケンスなど、複数ステップにわたるユースケースのシナリオ実行ロジック。

* **`Services/`**  : 
アプリケーション固有のユースケース処理の実装。
---

### 3. Domain Layer (ドメイン層)
外部フレームワークやデータベース、UIに依存しない、システムの核心となるビジネスロジックおよび抽象化インターフェースを定義します。

* **`Models/`**  :
 システムのドメインエンティティおよび値オブジェクト。

* **`MeasureCondition.cs`**  :
 測定データおよび検査条件を表現する核心モデル。

* **`Interfaces/`**  :
 ハードウェア制御や外部サービスの抽象化インターフェース。

* **`ICaioNative.cs`**  : 
AIO/DIO 制御用ネイティブ DLL を抽象化するインターフェース。パラメータに enum (例: AiRange) を用いることで型安全な制御を実現。

* **`Services/`**  : 
エンティティ単体に収まらないドメイン独自の領域ロジックや計算処理を担当するドメインサービス。
---

### 4. Infrastructure Layer (インフラストラクチャ層)
OS 依存処理、物理ファイル I/O、ハードウェア DLL 通信などの外部依存を具体的に実装・カプセル化する層です。

* **`Configuration/`**  :
 アプリケーションの環境設定・永続化処理。

* **`Settings.cs`**  :
 XML や App.config を利用した設定値の読み書き処理。

* **`Devices/`**  :
 実際の測定器やハードウェアデバイスとの通信・制御を行う具体的な実装クラス群。

* **`Mocks/`**  :
 実機が存在しない開発・テスト環境用ダミー機器・モックサービス群。

* **`NativeLibs/`**  :
 C/C++ 等で作成された外部 DLL ライブラリとの連携実装。

* **`CaioNativeLib.cs`**  :
 コンテック製 AIO ボード用ライブラリ (caio.dll) との直接通信および ICaioNative の実装。

* **`Persistence/`**  :
 ファイル保存・データベース処理およびデータフォーマット変換。

* **`MeasureConditionCNV.cs`**  :
 設定テキスト（List.dat など）内の旧形式コマンド (iDh) から新形式 (iDb) への正規表現互換性変換・永続化処理。

* **`Services/`**  :
 ログ出力やシステム共通のインフラストラクチャサービス。

* **`Win32/`**  :
 OS（Windows）固有の低レイヤー API 呼び出し。

* **`Win32Api.cs`**  :
 Win32 API (FindWindow, PrintWindow, BitBlt 等) の P/Invoke 定義。
---

## ⚙ 設計における重要ルール

1. **P/Invoke・OS依存処理の集約**  
   Win32 API などの直接呼び出し (`[LibralyImport]`) は `Infrastructure/NativeLibs/Win32Api.cs` に隠蔽し、インターフェース経由で ViewModel や Application 層から呼び出す。
2. **型安全なパラメータ管理**  
   ハードウェア制御インターフェースでは、マジックナンバーを排除し、`Domain` 層に定義した `enum` を使用して可読性と保守性を高める。
3. **実機とモックの分離（DIによる切り替え）**
   開発・テスト時には Infrastructure/Mocks/ 内のモック実装を DI コンテナ（DependencyInjection.cs）経由で注入することで、ハードウェア非接続環境でもアプリケーション動作を検証可能とする。
