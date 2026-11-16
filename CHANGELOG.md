<details open>
<summary>EN</summary>





## [2.0.0 (2026-11-11)](https://github.com/IGAWorksDev/adbrix-unity-sdk/releases/tag/v2.0.0)

##### Added
- Added Swift Package Manager (SPM) support for iOS dependency management. [iOS]
- Added `play-services-appset` dependency for App Set ID support. [Android]
  ```groovy
  implementation("com.google.android.gms:play-services-appset:16.0.2")
  ```
- Added iOS UIScene lifecycle support for deep link handling. [iOS]
  - Supports `scene:willConnectToSession:options:` for cold start deep links
  - Supports `scene:openURLContexts:` for scheme deep links
  - Supports `scene:continueUserActivity:` for Universal Links
  - Automatic detection and swizzling of SceneDelegate class
- Added Unity External Dependency Manager (UDM) support alongside EDM4U.

##### Changed
- Android SDK updated to version 4.1.0.
- Android Unity Extension updated to version 1.0.4.
- iOS SDK updated to version 3.2.5 (SPM) / 3.2.3 (CocoaPods).

##### Fixed
- Fixed an issue where deep link URL was not captured on cold start due to SDK initialization timing. [iOS]
  - Deep link URLs arriving before SDK initialization are now buffered and processed after initialization completes.
  - Applies to both Info.plist-based and C# script-based initialization methods.

##### Notes
- [CocoaPods Specs repository will become read-only in December 2026](https://blog.cocoapods.org/CocoaPods-Specs-Repo/). SPM is recommended for Unity iOS builds. Existing CocoaPods configuration remains available with version 3.2.3.
- This update addresses Apple TN3187 compliance for Xcode 27+ / iOS 27+ where UIScene lifecycle adoption is mandatory.
- Unity 2022.3.72f1+ and Unity 6000.x use UIScene-based lifecycle by default. This SDK version ensures deep link tracking works correctly in these environments.
- Legacy AppDelegate-based deep link handling remains functional for backward compatibility.

##### Versions

- **Android**
  - Adbrix Android SDK 4.1.0
  - Adbrix Android Unity Extension 1.0.4
- **iOS**
  - Adbrix iOS SDK 3.2.5 (Swift Package Manager)
  - Adbrix iOS SDK 3.2.3 (CocoaPods)


## 1.2.2 (2026-02-19)

##### Changed
- Logging system changed [iOS]

##### Fixed
- Fixed an issue in Unity where float values (e.g., 100.0) were incorrectly converted to integers, causing event attributes to be dropped. [Android]
- Resolved a NullPointerException that occurred in unit test environments when null values were passed as inputs. [Android]
- Fixed an intermittent issue where queued events were excluded from transmission during consecutive event calls. [Android]

##### Notes
- Event sampling now applies to all events. [Android]
- Standardized inconsistent log structures and messages across all platforms into a single format. [Android]

##### Versions

- **Android**
  - Adbrix Android SDK 3.0.10
  - Adbrix Android Unity Extension 1.0.2
- **iOS**
  - Adbrix iOS SDK 3.2.1
## 1.2.1 (2025-12-18)

##### Fixed
- Bug fixed

##### Versions

- **Android**
  - Adbrix Android SDK 3.0.7
  - Adbrix Android Unity Extension 1.0.2
- **iOS**
  - Adbrix iOS SDK 3.1.0
## 1.2.0 (2025-11-06)

##### Added
- Added support for SKAdNetwork (SKAN). [iOS]

##### Versions

- **Android**
  - Adbrix Android SDK 3.0.7
  - Adbrix Android Unity Extension 1.0.2
- **iOS**
  - Adbrix iOS SDK 3.1.0
## 1.1.7 (2025-09-02)

##### Fixed
- Fixed validation issue where invalid event property types were allowed. [iOS]

##### Enhancement
- Improving Event Collection Stability and Collection Rate [Android]

##### Versions

- **Android**
  - Adbrix Android SDK 3.0.7
- **iOS**
  - Adbrix iOS SDK 3.0.3
## 1.1.6 (2025-07-01)

##### Notes

- Updated the Adbrix Android SDK from 3.0.4 to 3.0.5.

##### Versions

- **Android**
  - Adbrix Android SDK 3.0.5
  - Adbrix Android Unity Extension 1.0.2
- **iOS**
  - Adbrix iOS SDK 3.0.2


## 1.1.5 (2025-06-18)

##### Fixed

- Fixed an issue where item properties were missing when delivering items with custom key-value pairs on iOS.

##### Notes

- Updated the Adbrix iOS SDK from 3.0.1 to 3.0.2.

##### Versions

- **Android**
  - Adbrix Android SDK 3.0.4
  - Adbrix Android Unity Extension 1.0.2
- **iOS**
  - Adbrix iOS SDK 3.0.2


## 1.1.4 (2025-06-13)

##### Fixed

- Fixed an issue where session check might not be performed upon re-entry after initialization with a C# script on Android.

##### Notes

- Updated the Adbrix Android SDK from 3.0.3 to 3.0.4.
- Updated the Adbrix Unity Extension from 1.0.1 to 1.0.2.

##### Versions

- **Android**
  - Adbrix Android SDK 3.0.4
  - Adbrix Android Unity Extension 1.0.2
- **iOS**
  - Adbrix iOS SDK 3.0.1


## 1.1.3 (2025-06-10)

##### Notes

- Updated the Adbrix Android SDK from 3.0.2 to 3.0.3.

##### Versions

- **Android**
  - Adbrix Android SDK 3.0.3
  - Adbrix Android Unity Extension 1.0.1
- **iOS**
  - Adbrix iOS SDK 3.0.1


## 1.1.2 (2025-06-05)

##### Notes

- Updated the Adbrix Android SDK from 3.0.1 to 3.0.2.

##### Versions

- **Android**
  - Adbrix Android SDK 3.0.2
  - Adbrix Android Unity Extension 1.0.1
- **iOS**
  - Adbrix iOS SDK 3.0.1


## 1.1.1 (2025-05-20)

##### Added

- Added [com.google.android.gms:play-services-ads-identifier](https://developers.google.com/android/guides/setup) dependency to Android platform in External Dependency Manager for Unity.

##### Versions

- **Android**
  - Adbrix Android SDK 3.0.1
  - Adbrix Android Unity Extension 1.0.1
- **iOS**
  - Adbrix iOS SDK 3.0.1


## 1.1.0 (2025-05-13)

##### Notes

- Added iOS interface: `public static void ATTAuthorized(bool isAuthorized)`
- Updated SDK patch versions for Android and iOS

##### Versions

- **Android**
  - Adbrix Android SDK 3.0.1
  - Adbrix Android Unity Extension 1.0.0
- **iOS**
  - Adbrix iOS SDK 3.0.1


## 1.0.0 (2025-04-24)

##### Notes

- Initial release

##### Versions

- **Android**
  - Adbrix Android SDK 3.0.0
  - Adbrix Android Unity Extension 1.0.0
- **iOS**
  - Adbrix iOS SDK 3.0.0

</details>
<details open>
<summary>KO</summary>





## [2.0.0 (2026-11-11)](https://github.com/IGAWorksDev/adbrix-unity-sdk/releases/tag/v2.0.0)

##### 추가됨
- iOS 의존성 관리를 위한 Swift Package Manager(SPM) 지원이 추가되었습니다. [iOS]
- App Set ID 지원을 위한 `play-services-appset` 의존성 추가. [Android]
  ```groovy
  implementation("com.google.android.gms:play-services-appset:16.0.2")
  ```
- iOS UIScene 생명주기 기반 딥링크 처리 지원 추가. [iOS]
  - `scene:willConnectToSession:options:` - 콜드 스타트 딥링크 지원
  - `scene:openURLContexts:` - 스킴 딥링크 지원
  - `scene:continueUserActivity:` - 유니버설 링크 지원
  - SceneDelegate 클래스 자동 감지 및 swizzling
- Unity External Dependency Manager(UDM) 지원 추가 (기존 EDM4U와 함께 지원).

##### 변경됨
- Android SDK가 버전 4.1.0으로 업데이트되었습니다.
- Android Unity Extension이 버전 1.0.4로 업데이트되었습니다.
- iOS SDK가 버전 3.2.5 (SPM) / 3.2.3 (CocoaPods)으로 업데이트되었습니다.

##### 고쳐짐
- 콜드 스타트 시 SDK 초기화 타이밍 문제로 딥링크 URL이 누락되던 문제를 수정하였습니다. [iOS]
  - SDK 초기화 전에 도착한 딥링크 URL을 버퍼링하여 초기화 완료 후 처리하도록 변경되었습니다.
  - Info.plist 기반 초기화 및 C# 스크립트 기반 초기화 모두 적용됩니다.

##### 참고 사항
- [CocoaPods Specs 저장소가 2026년 12월 부로 읽기 전용으로 전환](https://blog.cocoapods.org/CocoaPods-Specs-Repo/)되므로 Unity iOS 빌드 시 SPM 사용을 권장합니다. 기존 CocoaPods 설정은 버전 3.2.3으로 계속 사용 가능합니다.
- Apple TN3187 대응: Xcode 27+ / iOS 27+ 에서 UIScene 생명주기 채택 필수화에 대응합니다.
- Unity 2022.3.72f1+ 및 Unity 6000.x는 기본적으로 UIScene 기반 생명주기를 사용합니다. 이 SDK 버전은 해당 환경에서 딥링크 트래킹이 정상 동작하도록 보장합니다.
- 기존 AppDelegate 기반 딥링크 처리는 하위 호환성을 위해 유지됩니다.

##### 버전 정보

- **Android**
  - Adbrix Android SDK 4.1.0
  - Adbrix Android Unity Extension 1.0.4
- **iOS**
  - Adbrix iOS SDK 3.2.5 (Swift Package Manager)
  - Adbrix iOS SDK 3.2.3 (CocoaPods)

## 1.2.2 (2026-02-19)

##### 변경됨
- 로깅 시스템 변경 [iOS]

##### 고쳐짐
- Unity에서 100.0과 같은 실수형 데이터가 정수형으로 변환되어 이벤트 속성에서 누락되던 타입을 수정하였습니다. [Android]
- 유닛 테스트 환경에서 null 값이 입력될 경우 NullPointerException이 발생하던 문제를 해결하였습니다. [Android]
- 연속적인 이벤트 발생 시, 메모리 큐에 대기 중인 이벤트가 발송 대상에서 간헐적으로 누락되던 문제를 해결하였습니다. [Android]

##### 주요 사항
- 이벤트 샘플링이 모든 이벤트에 적용되도록 변경되었습니다. [Android]
- 플랫폼별로 상이했던 로그 구조와 메시지가 통일된 규격으로 변경되었습니다. [Android]

##### 버전 정보

- **Android**
  - Adbrix Android SDK 3.0.10
  - Adbrix Android Unity Extension 1.0.2
- **iOS**
  - Adbrix iOS SDK 3.2.1
## 1.2.1 (2025-12-18)

##### 고쳐짐
- 버그 수정

##### 버전 정보

- **Android**
  - Adbrix Android SDK 3.0.7
  - Adbrix Android Unity Extension 1.0.2
- **iOS**
  - Adbrix iOS SDK 3.1.0
## 1.2.0 (2025-11-06)

##### 추가됨
- SKAdNetwork(SKAN) 기능이 추가되었습니다. [iOS]

##### 버전 정보

- **Android**
  - Adbrix Android SDK 3.0.7
  - Adbrix Android Unity Extension 1.0.2
- **iOS**
  - Adbrix iOS SDK 3.1.0
## 1.1.7 (2025-09-02)

##### 고쳐짐
- 검증에 통과되지 않아야 하는 이벤트 프로퍼티의 타입이 통과되던 문제 수정 [iOS]

##### 개선
- 이벤트 수집 안정성 및 수집률 향상 [Android]

##### 버전 정보

- **Android**
  - Adbrix Android SDK 3.0.7
- **iOS**
  - Adbrix iOS SDK 3.0.3
## 1.1.6 (2025-07-01)

##### 주요 사항

- Adbrix Android SDK가 3.0.4에서 3.0.5로 변경되었습니다.

##### 버전 정보

- **Android**
  - Adbrix Android SDK 3.0.5
  - Adbrix Android Unity Extension 1.0.2
- **iOS**
  - Adbrix iOS SDK 3.0.2


## 1.1.5 (2025-06-18)

##### 고쳐짐

- iOS에서 상품(Item) 전달 시 커스텀 키 밸류값이 들어가면 누락되는 문제를 해결했습니다.

##### 주요 사항

- Adbrix iOS SDK가 3.0.1에서 3.0.2로 변경되었습니다.

##### 버전 정보

- **Android**
  - Adbrix Android SDK 3.0.4
  - Adbrix Android Unity Extension 1.0.2
- **iOS**
  - Adbrix iOS SDK 3.0.2


## 1.1.4 (2025-06-13)

##### 고쳐짐

- Android에서 C# 스크립트로 초기화를 진행한 후 재진입시 세션 체크가 되지 않을 수 있는 문제를 해결했습니다.

##### 주요 사항

- Adbrix Android SDK가 3.0.3에서 3.0.4로 변경되었습니다.
- Adbrix Android Unity Extension가 1.0.1에서 1.0.2로 변경되었습니다.

##### 버전 정보

- **Android**
  - Adbrix Android SDK 3.0.4
  - Adbrix Android Unity Extension 1.0.2
- **iOS**
  - Adbrix iOS SDK 3.0.1


## 1.1.3 (2025-06-10)

##### 주요 사항

- Adbrix Android SDK가 3.0.2에서 3.0.3로 변경되었습니다.

##### 버전 정보

- **Android**
  - Adbrix Android SDK 3.0.3
  - Adbrix Android Unity Extension 1.0.1
- **iOS**
  - Adbrix iOS SDK 3.0.1


## 1.1.2 (2025-06-05)

##### 주요 사항

- Adbrix Android SDK가 3.0.1에서 3.0.2로 변경되었습니다.

##### 버전 정보

- **Android**
  - Adbrix Android SDK 3.0.2
  - Adbrix Android Unity Extension 1.0.1
- **iOS**
  - Adbrix iOS SDK 3.0.1


## 1.1.1 (2025-05-20)

##### 추가됨

External Dependency Manager for Unity의 Android 플랫폼에 [com.google.android.gms:play-services-ads-identifier](https://developers.google.com/android/guides/setup) 의존성 추가.

##### 버전 정보

- **Android**
  - Adbrix Android SDK 3.0.1
  - Adbrix Android Unity Extension 1.0.1
- **iOS**
  - Adbrix iOS SDK 3.0.1


## 1.1.0 (2025-05-13)

##### 주요 사항

- iOS용 인터페이스 추가: `public static void ATTAuthorized(bool isAuthorized)`
- Android, iOS SDK 패치 버전 업데이트

##### 버전 정보

- **Android**
  - Adbrix Android SDK 3.0.1
  - Adbrix Android Unity Extension 1.0.0
- **iOS**
  - Adbrix iOS SDK 3.0.1


## 1.0.0 (2025-04-24)

##### 주요 사항

- 최초 배포 

##### 버전 정보

- **Android**
  - Adbrix Android SDK 3.0.0
  - Adbrix Android Unity Extension 1.0.0
- **iOS**
  - Adbrix iOS SDK 3.0.0

</details>