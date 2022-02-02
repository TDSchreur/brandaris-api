module.exports = {
    displayName: 'dnb-monetair-Brandaris',
    preset: 'jest-preset-angular',
    setupFilesAfterEnv: ['<rootDir>/src/test-setup.ts'],
    globals: {
        'ts-jest': {
            tsconfig: '<rootDir>/tsconfig.spec.json',
            stringifyContentPathRegex: '\\.(html|svg)$',
        },
    },
    transform: {
        '^.+\\.(ts|js|html)$': 'jest-preset-angular',
    },
    snapshotSerializers: [
        'jest-preset-angular/build/serializers/no-ng-attributes',
        'jest-preset-angular/build/serializers/ng-snapshot',
        'jest-preset-angular/build/serializers/html-comment',
    ],
    reporters: [
        'default',
        [
            'jest-junit',
            {
                suiteName: 'Brandaris-FE Unittests',
                outputName: 'test-report.xml',
                outputDirectory: 'coverage',
                classNameTemplate: '{classname} / {title}',
                titleTemplate: '{classname} / {title}',
                ancestorSeparator: ' / ',
                usePathForSuiteName: 'true',
            },
        ],
        [
            'jest-sonar',
            {
                outputName: 'test-report-sonar.xml',
                reportedFilePath: 'absolute',
            },
        ],
    ],
    collectCoverage: true,
    collectCoverageFrom: ['**/*.ts', '!**/node_modules/**', '!**/dist/**', '!**/coverage/**'],
    coverageReporters: ['lcov', 'cobertura'], // lcov for SQ, cobertura for ADO
};
