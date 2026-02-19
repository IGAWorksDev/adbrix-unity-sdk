<details open>
<summary>EN</summary>





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