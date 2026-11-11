# Adbrix Unity SDK

Unity용 Adbrix SDK입니다. Android 및 iOS 네이티브 SDK의 C# 인터페이스와 Unity 기능을 제공합니다.

- [Adbrix User Guide](https://adbrix.gitbook.io/user-guide)
- [Adbrix Developer Guide](https://adbrix.gitbook.io/developer-guide/platform/unity)

## 요구 사항

- Unity 2019.4 이상
- External Dependency Manager (EDM4U 또는 UDM)

## 설치

### 1. 패키지 설치

Unity Package Manager를 통해 설치:

**Git URL 사용:**
```
Window > Package Manager > + > Add package from git URL...
```
```
https://github.com/IGAWorksDev/adbrix-unity-sdk.git
```

**로컬 경로 사용:**
```
Window > Package Manager > + > Add package from disk...
```
`package.json` 파일 선택

### 2. External Dependency Manager 설치

네이티브 의존성 해결을 위해 아래 중 하나를 설치해야 합니다:

#### 옵션 A: Unity External Dependency Manager (UDM) - 권장

Unity 2022.3 이상에서 사용 가능한 공식 패키지입니다.

```
Window > Package Manager > Unity Registry > External Dependency Manager
```

또는 `Packages/manifest.json`에 추가:
```json
{
  "dependencies": {
    "com.unity.external-dependency-manager": "2.1.0"
  }
}
```

자세한 내용: [Unity EDM 문서](https://docs.unity3d.com/Packages/com.unity.external-dependency-manager@2.1/manual/index.html)

#### 옵션 B: Google EDM4U

Unity 2019.4 ~ 2022.2 또는 UDM 미지원 환경에서 사용합니다.

[EDM4U 릴리스 페이지](https://github.com/googlesamples/unity-jar-resolver/releases)에서 최신 `.unitypackage` 다운로드 후 임포트합니다.

또는 OpenUPM을 통해 설치:
```
Window > Package Manager > + > Add package by name...
```
```
com.google.external-dependency-manager
```

자세한 내용: [EDM4U 문서](https://github.com/googlesamples/unity-jar-resolver)

### 3. 의존성 해결

패키지 설치 후 자동으로 네이티브 의존성이 해결됩니다. 수동 해결이 필요한 경우:

**Android:**
```
Assets > External Dependency Manager > Android Resolver > Force Resolve
```

**iOS:**
빌드 시 자동으로 CocoaPods/Swift Package Manager를 통해 해결됩니다.

## 네이티브 의존성

### Android
| 패키지 | 버전 |
|--------|------|
| com.igaworks.adbrix:android-sdk | 4.1.0 |
| com.igaworks.adbrix:unity-extension | 1.0.2 |
| com.android.installreferrer:installreferrer | 2.2 |
| com.google.android.gms:play-services-ads-identifier | 18.0.1 |
| com.google.android.gms:play-services-appset | 16.0.2 |

### iOS

**CocoaPods:**
| Pod | 버전 |
|-----|------|
| adbrix2.0 | 3.2.3 |

**Swift Package Manager:**
| 패키지 | 버전 |
|--------|------|
| [AdbrixSDK](https://github.com/IGAWorksDev/adbrix-ios-sdk) | 3.2.5 |

iOS Resolver 설정에서 CocoaPods 또는 Swift Package Manager 중 선택 가능합니다.

## iOS UIScene Lifecycle 지원

### 개요

SDK 2.0.0부터 iOS UIScene 생명주기 기반 딥링크 처리를 지원합니다. 이는 Apple TN3187 요구사항과 Unity 2022.3.72f1+ / Unity 6000.x의 Scene 기반 iOS 빌드에 대응합니다.

### 지원 환경

| Unity 버전 | iOS Lifecycle | SDK 동작 |
|-----------|---------------|----------|
| 2019.4 ~ 2022.3.71 | AppDelegate | AppDelegate 메서드로 딥링크 처리 |
| 2022.3.72f1+ | UIScene | SceneDelegate 메서드로 딥링크 처리 |
| 6000.x (Unity 6) | UIScene | SceneDelegate 메서드로 딥링크 처리 |

### 동작 방식

SDK는 앱 실행 시 자동으로 Scene 생명주기 사용 여부를 감지합니다:

1. **Scene lifecycle 감지됨**: SceneDelegate 메서드를 swizzle하여 딥링크 처리
   - `scene:willConnectToSession:options:` - 콜드 스타트 딥링크
   - `scene:openURLContexts:` - 스킴 딥링크 (백그라운드)
   - `scene:continueUserActivity:` - 유니버설 링크 (백그라운드)

2. **Scene lifecycle 미감지**: 기존 AppDelegate 메서드로 딥링크 처리 (하위 호환)

### 추가 설정 불필요

SDK가 자동으로 적절한 경로를 선택하므로 개발자의 추가 설정은 필요하지 않습니다.

### 참고 자료

- [Apple TN3187: Migrating to the UIKit scene-based life cycle](https://developer.apple.com/documentation/technotes/tn3187-migrating-to-the-uikit-scene-based-life-cycle)
- [Unity iOS UIScene lifecycle support](https://discussions.unity.com/t/info-apple-update-your-editor-to-receive-uiscene-lifecycle-support/1709065)
